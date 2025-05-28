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
    public DateTimeOffset PublicationDate { get; set; }
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