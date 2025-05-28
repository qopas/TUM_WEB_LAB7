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
                throw new ApplicationException("User with this email already exists");

            return await unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var user = CreateUser(request);
                var createResult = await userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                    throw new ApplicationException(string.Join(", ", createResult.Errors.Select(e => e.Description)));

                var roleResult = await userManager.AddToRoleAsync(user, "Customer");
                if (!roleResult.Succeeded)
                    throw new ApplicationException(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                
                var customer = CreateCustomer(request, user);

                await unitOfWork.Customers.AddAsync(customer);
                await unitOfWork.SaveChangesAsync();
                
                user.CustomerId = customer.Id;
                var updateResult = await userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    throw new ApplicationException(string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                
                return await tokenGenerationService.GenerateAuthenticationResult(user);
            });
        }

        private static BookRental.Domain.Entities.Customer CreateCustomer(RegisterCommand request, ApplicationUser user)
        {
            var customer = new BookRental.Domain.Entities.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                ApplicationUserId = user.Id
            };
            return customer;
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