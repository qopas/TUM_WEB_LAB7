using Application.DTOs.Authentication;
using FluentValidation;
using MediatR;

namespace Application.Authentication.Commands.Login;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}