using BookRental.Domain.Common;
using BookRental.Domain.Entities;
using BookRental.Domain.Entities.Models;

namespace BookRental.Domain.Interfaces.Services;

public interface IUserService
{
    string? CurrentUserId { get; }
    Task<Result<ApplicationUser>> LoginAsync(string email, string password);
    Task<Result<ApplicationUser>> RegisterAsync(UserRegistrationModel registrationModel);
    Task<Result<ApplicationUser>> RefreshTokenAsync(string token, string refreshToken);
    Task<Result<bool>> LogoutAsync(string refreshToken);
}
