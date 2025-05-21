using Application.DTOs.Rent;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Rent.Commands.CreateRent;

public class CreateRentCommand : IRequest<RentDto>
{
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
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
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Rent date cannot be in the future");

        RuleFor(r => r.DueDate)
            .NotEmpty().WithMessage("Due date is required")
            .GreaterThan(r => r.RentDate).WithMessage("Due date must be after the rent date");
    }
}