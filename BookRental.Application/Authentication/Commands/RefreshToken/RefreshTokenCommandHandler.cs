using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTOs.Authentication;
using Application.Service;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork,
        ITokenGenerationService tokenGenerationService,
        TokenValidationParameters tokenValidationParameters)
        : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateTokensAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ApplicationException(validationResult.Error);

            return await unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                validationResult.RefreshToken.Used = true;
                await unitOfWork.SaveChangesAsync();

                var user = await userManager.FindByIdAsync(validationResult.UserId);
                if (user == null)
                    throw new ApplicationException("User not found");

                return await tokenGenerationService.GenerateAuthenticationResult(user);
            });
        }

        private async Task<ValidationResult> ValidateTokensAsync(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var claimsPrincipal = GetPrincipalFromToken(request.Token);
            if (claimsPrincipal == null)
                return ValidationResult.Failure("Invalid token");
            
            var expiryDateUnix = long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTimeOffset.UtcNow)
                return ValidationResult.Failure("This token hasn't expired yet");
            
            var storedRefreshToken = await unitOfWork.RefreshTokens
                .Find(rt => rt.Token == request.RefreshToken)
                .FirstOrDefaultAsync(cancellationToken);

            if (storedRefreshToken == null)
                return ValidationResult.Failure("This refresh token does not exist");
            
            if (DateTimeOffset.UtcNow > storedRefreshToken.ExpiryDate)
                return ValidationResult.Failure("This refresh token has expired");

            if (storedRefreshToken.Invalidated)
                return ValidationResult.Failure("This refresh token has been invalidated");

            if (storedRefreshToken.Used)
                return ValidationResult.Failure("This refresh token has been used");

            var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedRefreshToken.JwtId != jti)
                return ValidationResult.Failure("This refresh token does not match this JWT");

            var userId = claimsPrincipal.Claims.Single(x => x.Type == "id").Value;
            
            return ValidationResult.Success(userId, storedRefreshToken);
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

        private record ValidationResult(bool IsValid, string Error, string UserId, BookRental.Domain.Entities.RefreshToken RefreshToken)
        {
            public static ValidationResult Success(string userId, BookRental.Domain.Entities.RefreshToken refreshToken) 
                => new(true, string.Empty, userId, refreshToken);
            
            public static ValidationResult Failure(string error) 
                => new(false, error, string.Empty, null!);
        }
    }
}