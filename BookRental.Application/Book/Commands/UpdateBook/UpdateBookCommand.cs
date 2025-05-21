using FluentValidation;
using MediatR;

namespace Application.Mediator.Book.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
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
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Publication date cannot be in the future");

        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("Genre ID is required");

        RuleFor(x => x.AvailableQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Available quantity cannot be negative");

        RuleFor(x => x.RentalPrice)
            .GreaterThan(0).WithMessage("Rental price must be greater than zero");
    }
}