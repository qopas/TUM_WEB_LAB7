using Application.DTOs.Authentication;
using Application.Service;
using BookRental.Domain.Entities;
using BookRental.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Login
{
    public class LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenGenerationService tokenGenerationService)
        : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
    {
        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result<AuthResponseDto>.Failure(["Invalid login credentials"]);

            if (!user.LockoutEnabled)
                return Result<AuthResponseDto>.Failure(["User account is deactivated"]);

            var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                return Result<AuthResponseDto>.Failure(["Invalid login credentials"]);

            user.LastLoginAt = DateTimeOffset.UtcNow;
            await userManager.UpdateAsync(user);

            var tokenResult = await tokenGenerationService.GenerateAuthenticationResult(user);
            return tokenResult;
        }
    }
}
