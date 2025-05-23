
using Application.DTOs.Authentication;
using Application.Service;
using BookRental.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Login
{
    public class LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenGenerationService tokenGenerationService)
        : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return AuthResponseDto.CreateFailure(new[] { "Invalid login credentials" });

                if (!user.LockoutEnabled)
                    return AuthResponseDto.CreateFailure(new[] { "User account is deactivated" });

                var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
                if (!passwordValid)
                    return AuthResponseDto.CreateFailure(new[] { "Invalid login credentials" });

                user.LastLoginAt = DateTime.UtcNow;
                await userManager.UpdateAsync(user);

                return await tokenGenerationService.GenerateAuthenticationResult(user);
            }
            catch (Exception ex)
            {
                return AuthResponseDto.CreateFailure(new[] { "An error occurred during login" + ex.Message });
            } 
        }
    }
}