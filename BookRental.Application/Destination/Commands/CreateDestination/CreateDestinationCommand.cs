using System.Text.RegularExpressions;
using Application.DTOs.Destination;
using FluentValidation;
using MediatR;

namespace Application.Destination.Commands.CreateDestination;

public class CreateDestinationCommand : IRequest<DestinationDto>
{
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required string City { get; init; }
    public required string ContactPerson { get; init; }
    public required string PhoneNumber { get; init; }
}

public class CreateDestinationCommandValidator : AbstractValidator<CreateDestinationCommand>
{
    public CreateDestinationCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Destination name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(d => d.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(d => d.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters");

        RuleFor(d => d.ContactPerson)
            .NotEmpty().WithMessage("Contact person is required")
            .MaximumLength(100).WithMessage("Contact person name cannot exceed 100 characters");

        RuleFor(d => d.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(new Regex(@"^\+?[0-9\s-()]{7,20}$")).WithMessage("A valid phone number is required")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");
    }
}