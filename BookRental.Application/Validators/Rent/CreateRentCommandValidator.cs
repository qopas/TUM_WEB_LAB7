using Application.Mediator.Rent.Commands.CreateRent;
using BookRental.Domain.Interfaces;
using FluentValidation;

namespace Application.Validators.Rent;

 public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
    {
        public CreateRentCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(r => r.BookId)
                .NotEmpty().WithMessage("Book ID is required")
                .MustAsync(async (bookId, cancellation) => {
                    var book = await unitOfWork.Books.GetByIdAsync(bookId);
                    return book != null;
                }).WithMessage("Book not found")
                .MustAsync(async (bookId, cancellation) => {
                    var book = await unitOfWork.Books.GetByIdAsync(bookId);
                    return book != null && book.AvailableQuantity > 0;
                }).WithMessage("Book is not available for rent (no copies left)");

            RuleFor(r => r.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required")
                .MustAsync(async (customerId, cancellation) => {
                    var customer = await unitOfWork.Customers.GetByIdAsync(customerId);
                    return customer != null;
                }).WithMessage("Customer not found");

            RuleFor(r => r.DestinationId)
                .NotEmpty().WithMessage("Destination ID is required")
                .MustAsync(async (destinationId, cancellation) => {
                    var destination = await unitOfWork.Destinations.GetByIdAsync(destinationId);
                    return destination != null;
                }).WithMessage("Destination not found");

            RuleFor(r => r.RentDate)
                .NotEmpty().WithMessage("Rent date is required")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Rent date cannot be in the future");

            RuleFor(r => r.DueDate)
                .NotEmpty().WithMessage("Due date is required")
                .GreaterThan(r => r.RentDate).WithMessage("Due date must be after the rent date");
        }
    }