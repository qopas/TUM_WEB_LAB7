using BookRental.Domain.Common;
using BookRental.Domain.Entities;
using BookRental.Domain.Entities.Models;

namespace BookRental.Domain.Interfaces.Services;

public interface ITokenGenerationService
{
    Task<Result<AuthModel>> GenerateAuthenticationResult(ApplicationUser user);
}