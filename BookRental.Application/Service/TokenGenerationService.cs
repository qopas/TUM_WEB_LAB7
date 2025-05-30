using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Jwt;
using BookRental.Domain.Common;
using BookRental.Domain.Entities;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Service;

public class TokenGenerationService(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IOptions<JwtSettings> jwtSettings)
    : ITokenGenerationService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<Result<AuthModel>> GenerateAuthenticationResult(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        
        var now = DateTimeOffset.UtcNow;
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
        };
        
        var userRoles = await userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now.UtcDateTime,
            IssuedAt = now.UtcDateTime,
            Expires = now.AddMinutes(_jwtSettings.TokenLifetime).UtcDateTime,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var refreshTokenModel = new RefreshTokenModel
        {
            JwtId = token.Id,
            UserId = user.Id,
            CreationDate = now,
            ExpiryDate = now.AddDays(_jwtSettings.RefreshTokenLifetime),
            Token = Guid.NewGuid().ToString()
        };

        var refreshTokenResult = RefreshToken.Create(refreshTokenModel);
        if (!refreshTokenResult.IsSuccess)
            return Result<AuthModel>.Failure(refreshTokenResult.Errors);
        
        var createdRefreshToken = await unitOfWork.RefreshTokens.CreateAsync(refreshTokenResult.Value);
        await unitOfWork.SaveChangesAsync();

        var authModel = new AuthModel
        {
            Token = tokenString,
            RefreshToken = createdRefreshToken.Token,
            UserId = user.Id,
            CustomerId = user.CustomerId
        };

        return Result<AuthModel>.Success(authModel);
    }
}