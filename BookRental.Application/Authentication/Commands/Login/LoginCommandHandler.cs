using Application.DTOs.Authentication;
using Application.Exceptions;
using BookRental.Domain.Common;
using BookRental.Domain.Interfaces.Services;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Authentication.Commands.Login;

public class LoginCommandHandler(IUserService userService, ITokenGenerationService tokenGenerationService)
    : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userResult = await userService.LoginAsync(request.Email, request.Password);
        if (!userResult.IsSuccess)
            throw new UnauthorizedException("Incorrect email or password");

        var tokenResult = await tokenGenerationService.GenerateAuthenticationResult(userResult.Value);
        if (!tokenResult.IsSuccess)
            throw new ApplicationException(tokenResult.Errors);

        var authResponse = AuthResponseDto.CreateSuccess(
            tokenResult.Value.Token,
            tokenResult.Value.RefreshToken,
            tokenResult.Value.UserId,
            tokenResult.Value.CustomerId);

        return Result<AuthResponseDto>.Success(authResponse);
    }
}