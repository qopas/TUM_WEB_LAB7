using Application.DTOs.Authentication;
using BookRental.Domain.Entities;

namespace Application.Service;

public interface ITokenGenerationService
{
    Task<AuthResponseDto> GenerateAuthenticationResult(ApplicationUser user);
}