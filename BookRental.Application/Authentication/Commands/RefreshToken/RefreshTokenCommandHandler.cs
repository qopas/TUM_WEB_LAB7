using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Authentication;
using Application.Jwt;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IOptions<JwtSettings> jwtSettings,
    TokenValidationParameters tokenValidationParameters)
    : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var claimsPrincipal = GetPrincipalFromToken(request.Token);
            if (claimsPrincipal == null)
                return AuthResponseDto.CreateFailure(new[] { "Invalid token" });

      
            var expiryDateUnix = long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return AuthResponseDto.CreateFailure(new[] { "This token hasn't expired yet" });
            
            var storedRefreshToken = await unitOfWork.RefreshTokens
                .Find(rt => rt.Token == request.RefreshToken)
                .FirstOrDefaultAsync(cancellationToken);

            if (storedRefreshToken == null)
                return AuthResponseDto.CreateFailure(new[] { "This refresh token does not exist" });
            
            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return AuthResponseDto.CreateFailure(new[] { "This refresh token has expired" });

            if (storedRefreshToken.Invalidated)
                return AuthResponseDto.CreateFailure(new[] { "This refresh token has been invalidated" });

            if (storedRefreshToken.Used)
                return AuthResponseDto.CreateFailure(new[] { "This refresh token has been used" });

            var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedRefreshToken.JwtId != jti)
                return AuthResponseDto.CreateFailure(new[] { "This refresh token does not match this JWT" });
            
            storedRefreshToken.Used = true;
            await unitOfWork.RefreshTokens.UpdateAsync(storedRefreshToken);
            await unitOfWork.SaveChangesAsync();
            
            var userId = claimsPrincipal.Claims.Single(x => x.Type == "id").Value;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return AuthResponseDto.CreateFailure(new[] { "User not found" });
            
            return await GenerateAuthenticationResult(user);
        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthResponseDto> GenerateAuthenticationResult(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id),
                new Claim("customerId", user.CustomerId ?? "")
            };
            
            var userRoles = await userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            var refreshToken = new BookRental.Domain.Entities.RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetime),
                Token = Guid.NewGuid().ToString()
            };
            
            await unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await unitOfWork.SaveChangesAsync();

            return AuthResponseDto.CreateSuccess(
                tokenString,
                refreshToken.Token,
                user.Id,
                user.CustomerId
            );
        }
    }