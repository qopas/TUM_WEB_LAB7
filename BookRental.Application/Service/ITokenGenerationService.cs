using Application.DTOs.Authentication;
using BookRental.Domain.Common;
using BookRental.Domain.Entities;

namespace Application.Service;

public interface ITokenGenerationService
{
    Task<Result<AuthResponseDto>> GenerateAuthenticationResult(ApplicationUser user);
}