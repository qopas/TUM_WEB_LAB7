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

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IOptions<JwtSettings> jwtSettings)
    : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    
    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return AuthResponseDto.CreateFailure(new[] { "User with this email already exists" });
            
            var user = await CreateUser(request);
            if (user == null)
                return AuthResponseDto.CreateFailure(new[] { "Failed to create user" });
            
            var customer = await CreateCustomer(request, user.Id);
            
            user.CustomerId = customer.Id;
            await userManager.UpdateAsync(user);
            
            return await GenerateAuthenticationResult(user);
        }

        private async Task<ApplicationUser?> CreateUser(RegisterCommand request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createResult = await userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                return null;

            await userManager.AddToRoleAsync(user, "Customer");
            return user;
        }

        private async Task<BookRental.Domain.Entities.Customer> CreateCustomer(RegisterCommand request, string userId)
        {
            var customer = new BookRental.Domain.Entities.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                ApplicationUserId = userId
            };

            await unitOfWork.Customers.AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
            
            return customer;
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