using Application.Mediator.Rent.Commands.CreateRent;
using BookRental.Domain.Interfaces;
using FluentValidation;

namespace Application.Validators.Rent;

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