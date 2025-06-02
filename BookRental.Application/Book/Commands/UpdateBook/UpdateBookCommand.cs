using BookRental.Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Book.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required DateTimeOffset PublicationDate { get; init; }
    public required IEnumerable<string> GenreIds { get; init; }
    public required int AvailableQuantity { get; init; }
    public required decimal RentalPrice { get; init; }
}
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
            .LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("Publication date cannot be in the future");

        RuleFor(x => x.GenreIds)
            .NotEmpty().WithMessage("At least one genre is required")
            .Must(ids => ids.All(id => Guid.TryParse(id, out _)))
            .WithMessage("All genre IDs must be valid GUIDs");

        RuleFor(x => x.AvailableQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Available quantity cannot be negative");

        RuleFor(x => x.RentalPrice)
            .GreaterThan(0).WithMessage("Rental price must be greater than zero");
    }
}