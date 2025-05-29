using Application.DTOs.Authentication;
using Application.Service;
using BookRental.Domain.Entities;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork,
        ITokenGenerationService tokenGenerationService)
        : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
    {
        public async Task<Result<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result<AuthResponseDto>.Failure(["User with this email already exists"]);

            return await unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var user = CreateUser(request);
                var createResult = await userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                    return Result<AuthResponseDto>.Failure(createResult.Errors.Select(e => e.Description).ToList());

                var roleResult = await userManager.AddToRoleAsync(user, "Customer");
                if (!roleResult.Succeeded)
                    return Result<AuthResponseDto>.Failure(roleResult.Errors.Select(e => e.Description).ToList());

                var customerResult = CreateCustomer(request, user);
                if (!customerResult.IsSuccess)
                    return Result<AuthResponseDto>.Failure(customerResult.Errors);

                var createdCustomer = await unitOfWork.Customers.AddAsync(customerResult.Value);
                await unitOfWork.SaveChangesAsync();

                user.CustomerId = createdCustomer.Id;
                var updateResult = await userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return Result<AuthResponseDto>.Failure(updateResult.Errors.Select(e => e.Description).ToList());

                var tokenResult = await tokenGenerationService.GenerateAuthenticationResult(user);
                return tokenResult;
            });
        }

        private static Result<BookRental.Domain.Entities.Customer> CreateCustomer(RegisterCommand request, ApplicationUser user)
        {
            var customerModel = new CustomerModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                ApplicationUserId = user.Id
            };
            return BookRental.Domain.Entities.Customer.Create(customerModel);
        }

        private static ApplicationUser CreateUser(RegisterCommand request)
        {
            return new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTimeOffset.Now
            };
        }
    }
}
