using Application.Authentication.Commands.Register;
using Application.DTOs.Authentication;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Interfaces.Services;
using MediatR;

public class RegisterCommandHandler(IUserService userService, ITokenGenerationService tokenGenerationService) 
    : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registrationModel = new UserRegistrationModel
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            City = request.City,
            PhoneNumber = request.PhoneNumber
        };

        var userResult = await userService.RegisterAsync(registrationModel);
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