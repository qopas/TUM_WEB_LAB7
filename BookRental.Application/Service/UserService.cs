using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookRental.Domain;
using BookRental.Domain.Common;
using BookRental.Domain.Entities;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Service;

public class UserService(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    TokenValidationParameters tokenValidationParameters,
    IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    public async Task<Result<ApplicationUser>> LoginAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null || !user.LockoutEnabled)
            return Result<ApplicationUser>.Failure(["Invalid login credentials"]);

        if (!await userManager.CheckPasswordAsync(user, password))
            return Result<ApplicationUser>.Failure(["Invalid login credentials"]);

        user.LastLoginAt = DateTimeOffset.UtcNow;
        await userManager.UpdateAsync(user);

        return Result<ApplicationUser>.Success(user);
    }

    public async Task<Result<ApplicationUser>> RegisterAsync(UserRegistrationModel model)
    {
        if (await userManager.FindByEmailAsync(model.Email) != null)
            return Result<ApplicationUser>.Failure(["User with this email already exists"]);

        return await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var createResult = await userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
                return Result<ApplicationUser>.Failure(createResult.Errors.Select(e => e.Description).ToList());

            var roleResult = await userManager.AddToRoleAsync(user, Constants.RoleCustomer);
            if (!roleResult.Succeeded)
                return Result<ApplicationUser>.Failure(roleResult.Errors.Select(e => e.Description).ToList());

            var customerModel = new CustomerModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                ApplicationUserId = user.Id
            };

            var customerResult = BookRental.Domain.Entities.Customer.Create(customerModel);
            if (!customerResult.IsSuccess)
                return Result<ApplicationUser>.Failure(customerResult.Errors);

            var createdCustomer = await unitOfWork.Customers.CreateAsync(customerResult.Value);
            await unitOfWork.SaveChangesAsync();

            user.CustomerId = createdCustomer.Id;
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return Result<ApplicationUser>.Failure(updateResult.Errors.Select(e => e.Description).ToList());

            return Result<ApplicationUser>.Success(user);
        });
    }

    public async Task<Result<ApplicationUser>> RefreshTokenAsync(string token, string refreshToken)
    {
        var tokenValidationResult = await ValidateTokensAsync(token, refreshToken);
        if (!tokenValidationResult.IsSuccess)
            return Result<ApplicationUser>.Failure(tokenValidationResult.Errors);

        return await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            await unitOfWork.RefreshTokens.UpdateAsync(tokenValidationResult.Value,
                setters => setters.SetProperty(rt => rt.Used, true));

            await unitOfWork.SaveChangesAsync();

            var userId = GetPrincipalFromToken(token)?.Claims
                .FirstOrDefault(x => x.Type == "id")?.Value;

            var user = await userManager.FindByIdAsync(userId);
            return user == null
                ? Result<ApplicationUser>.Failure(["User not found"])
                : Result<ApplicationUser>.Success(user);
        });
    }

    public async Task<Result<bool>> LogoutAsync(string refreshToken)
    {
        if (!string.IsNullOrEmpty(refreshToken))
        {
            await unitOfWork.RefreshTokens.UpdateAsync(refreshToken,
                setters => setters.SetProperty(rt => rt.Invalidated, true));
        }

        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    private async Task<Result<string>> ValidateTokensAsync(string token, string refreshToken)
    {
        var claimsPrincipal = GetPrincipalFromToken(token);
        if (claimsPrincipal == null)
            return Result<string>.Failure(["Invalid token"]);

        var expiryDateUnix = long.Parse(claimsPrincipal.Claims.First(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix);

        if (expiryDate > DateTimeOffset.UtcNow)
            return Result<string>.Failure(["This token hasn't expired yet"]);

        var storedRefreshToken = await unitOfWork.RefreshTokens
            .Find(rt => rt.Token == refreshToken)
            .FirstOrDefaultAsync();

        if (storedRefreshToken == null)
            return Result<string>.Failure(["This refresh token does not exist"]);

        var validationErrors = new List<string>();

        if (DateTimeOffset.UtcNow > storedRefreshToken.ExpiryDate)
            validationErrors.Add("This refresh token has expired");
        if (storedRefreshToken.Invalidated)
            validationErrors.Add("This refresh token has been invalidated");
        if (storedRefreshToken.Used)
            validationErrors.Add("This refresh token has been used");

        var jti = claimsPrincipal.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        if (storedRefreshToken.JwtId != jti)
            validationErrors.Add("This refresh token does not match this JWT");

        return validationErrors.Any()
            ? Result<string>.Failure(validationErrors)
            : Result<string>.Success(storedRefreshToken.Id);
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            return IsJwtWithValidSecurityAlgorithm(validatedToken) ? principal : null;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) =>
        validatedToken is JwtSecurityToken jwtSecurityToken &&
        jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
}