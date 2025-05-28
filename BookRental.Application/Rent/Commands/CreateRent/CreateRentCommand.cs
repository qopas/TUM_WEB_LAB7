using Application.DTOs.Rent;
using FluentValidation;
using MediatR;

namespace Application.Rent.Commands.CreateRent;

public class CreateRentCommand : IRequest<RentDto>
{
    public required string BookId { get; init; }
    public required string CustomerId { get; init; }
    public required string DestinationId { get; init; }
    public required DateTimeOffset RentDate { get; init; }
    public required DateTimeOffset DueDate { get; init; }
}
public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
{
    public CreateRentCommandValidator()
    {
        RuleFor(r => r.BookId)
            .NotEmpty().WithMessage("Book ID is required");

        RuleFor(r => r.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required");
        RuleFor(r => r.DestinationId)
            .NotEmpty().WithMessage("Destination ID is required");

        RuleFor(r => r.RentDate)
            .NotEmpty().WithMessage("Rent date is required")
            .LessThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage("Rent date cannot be in the future");

        RuleFor(r => r.DueDate)
            .NotEmpty().WithMessage("Due date is required")
            .GreaterThan(r => r.RentDate).WithMessage("Due date must be after the rent date");
    }
}