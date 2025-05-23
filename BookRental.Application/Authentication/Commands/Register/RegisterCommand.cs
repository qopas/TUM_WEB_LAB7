using Application.DTOs.Authentication;
using FluentValidation;
using MediatR;

namespace Application.Authentication.Commands.Register;

public class RegisterCommand : IRequest<AuthResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string? Address { get; init; }
    public required string? City { get; init; }
    public required string? PhoneNumber { get; init; }
}
public class RegisterCommandValidator : AbstractValidator<RegisterCommand> 
{
     public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("First name can only contain letters and spaces");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("Last name can only contain letters and spaces");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

            RuleFor(x => x.City)
                .MaximumLength(100).WithMessage("City must not exceed 100 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("City can only contain letters and spaces")
                .When(x => !string.IsNullOrEmpty(x.City));

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters")
                .Matches(@"^[\d\s\-\+\(\)]+$").WithMessage("Invalid phone number format")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
}