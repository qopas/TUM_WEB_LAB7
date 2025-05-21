using BookRental.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Rent.Commands.UpdateRent;

public class UpdateRentCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
}
public class UpdateRentCommandValidator : AbstractValidator<UpdateRentCommand>
{
    public UpdateRentCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Rent ID is required");

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

        RuleFor(r => r.ReturnDate)
            .Must((command, returnDate) => !returnDate.HasValue || returnDate >= command.RentDate)
            .WithMessage("Return date must be after or equal to the rent date")
            .When(r => r.ReturnDate.HasValue);

        RuleFor(r => r.Status)
            .IsInEnum().WithMessage("Invalid rent status");
            
        When(r => r.Status == RentStatus.Returned, () => {
            RuleFor(r => r.ReturnDate)
                .NotNull().WithMessage("Return date is required when status is 'Returned'");
        });

        When(r => r.ReturnDate.HasValue, () => {
            RuleFor(r => r.Status)
                .Equal(RentStatus.Returned).WithMessage("Status must be 'Returned' when return date is provided");
        });
    }
}