using Application.DTOs.Authentication;
using BookRental.Domain.Common;
using BookRental.Domain.Interfaces.Services;
using MediatR;

namespace Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IUserService userService, ITokenGenerationService tokenGenerationService)
    : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userResult = await userService.RefreshTokenAsync(request.Token, request.RefreshToken);
        if (!userResult.IsSuccess)
            return Result<AuthResponseDto>.Failure(userResult.Errors);

        var tokenResult = await tokenGenerationService.GenerateAuthenticationResult(userResult.Value);
        if (!tokenResult.IsSuccess)
            return Result<AuthResponseDto>.Failure(tokenResult.Errors);

        var authResponse = AuthResponseDto.CreateSuccess(
            tokenResult.Value.Token,
            tokenResult.Value.RefreshToken,
            tokenResult.Value.UserId,
            tokenResult.Value.CustomerId);

        return Result<AuthResponseDto>.Success(authResponse);
    }
}