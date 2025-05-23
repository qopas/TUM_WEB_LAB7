using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Authentication;
using Application.Jwt;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Commands.Login;

 public class LoginCommandHandler(
     UserManager<ApplicationUser> userManager,
     IUnitOfWork unitOfWork,
     IOptions<JwtSettings> jwtSettings)
     : IRequestHandler<LoginCommand, AuthResponseDto>
 {
     private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AuthResponseDto.CreateFailure(new[] { "Invalid login credentials" });
            
            if (!user.IsActive)
                return AuthResponseDto.CreateFailure(new[] { "User account is deactivated" });
            
            var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                return AuthResponseDto.CreateFailure(new[] { "Invalid login credentials" });
            
            user.LastLoginAt = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            
            return await GenerateAuthenticationResult(user);
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