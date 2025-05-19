using Application.Mediator.Book.Commands.UpdateBook;
using FluentValidation;

namespace Application.Validators.Book;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required")
            .MaximumLength(100).WithMessage("Author cannot exceed 100 characters");

        RuleFor(x => x.PublicationDate)
            .NotEmpty().WithMessage("Publication date is required")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Publication date cannot be in the future");

        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("Genre ID is required");

        RuleFor(x => x.AvailableQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Available quantity cannot be negative");

        RuleFor(x => x.RentalPrice)
            .GreaterThan(0).WithMessage("Rental price must be greater than zero");
    }
}