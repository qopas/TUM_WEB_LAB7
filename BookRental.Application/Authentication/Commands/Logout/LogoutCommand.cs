using FluentValidation;
using MediatR;

namespace Application.Authentication.Commands.Logout;

public class LogoutCommand : IRequest<bool>
{
    public required string UserId { get; init; }
    public required string? RefreshToken { get; init; }
}
public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");
    }
}