using Application.DTOs.Book;
using FluentValidation;
using MediatR;

namespace Application.Book.Commands.CreateBook;

public class CreateBookCommand : IRequest<BookDto>
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required DateTime PublicationDate { get; init; }
    public required string GenreId { get; init; }
    public required int AvailableQuantity { get; init; }
    public required decimal RentalPrice { get; init; }
}
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
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