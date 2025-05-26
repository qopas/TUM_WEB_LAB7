using Application.Book.Commands.CreateBook;
using Application.Book.Commands.UpdateBook;
using Application.DTOs.Book;
using FluentValidation;

namespace BookRental.Web.Models;

public class BookViewModel
{
    public string? Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    
    public static BookViewModel FromDto(BookDto dto)
    {
        return new BookViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Author = dto.Author,
            PublicationDate = dto.PublicationDate,
            GenreId = dto.GenreId,
            AvailableQuantity = dto.AvailableQuantity,
            RentalPrice = dto.RentalPrice
        };
    }

    public CreateBookCommand ToCreateCommand()
    {
        return new CreateBookCommand
        {
            Title = Title,
            Author = Author,
            PublicationDate = PublicationDate,
            GenreId = GenreId,
            AvailableQuantity = AvailableQuantity,
            RentalPrice = RentalPrice
        };
    }
    public UpdateBookCommand ToUpdateCommand()
    {
        return new UpdateBookCommand
        {
            Id = Id,
            Title = Title,
            Author = Author,
            PublicationDate = PublicationDate,
            GenreId = GenreId,
            AvailableQuantity = AvailableQuantity,
            RentalPrice = RentalPrice
        };
    }
}

public class BookViewModelValidator : AbstractValidator<BookViewModel>
{
    public BookViewModelValidator()
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
            .NotEmpty().WithMessage("Genre is required");

        RuleFor(x => x.AvailableQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Available quantity cannot be negative");

        RuleFor(x => x.RentalPrice)
            .GreaterThan(0).WithMessage("Rental price must be greater than 0");
    }
}