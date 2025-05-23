using Application.DTOs.Authentication;
using Application.Service;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork,
        ITokenGenerationService tokenGenerationService)
        : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return AuthResponseDto.CreateFailure(new[] { "User with this email already exists" });

            await unitOfWork.BeginTransactionAsync();
            try
            {
                var user = CreateUser(request);
                var createResult = await userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return AuthResponseDto.CreateFailure(createResult.Errors.Select(e => e.Description));
                }
                var roleResult = await userManager.AddToRoleAsync(user, "Customer");
                if (!roleResult.Succeeded)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return AuthResponseDto.CreateFailure(roleResult.Errors.Select(e => e.Description));
                }
                
                var customer = new BookRental.Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    City = request.City,
                    ApplicationUserId = user.Id
                };

                await unitOfWork.Customers.AddAsync(customer);
                await unitOfWork.SaveChangesAsync();
                
                user.CustomerId = customer.Id;
                var updateResult = await userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return AuthResponseDto.CreateFailure(updateResult.Errors.Select(e => e.Description));
                }
                
                var authResult = await tokenGenerationService.GenerateAuthenticationResult(user);
                
                await unitOfWork.CommitTransactionAsync();
                
                return authResult;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                return AuthResponseDto.CreateFailure(new[] { "An error occurred during registration" });
            }
        }

        private static ApplicationUser CreateUser(RegisterCommand request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };
            return user;
        }
    }
}