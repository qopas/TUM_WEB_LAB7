using Application.Book.Commands.CreateBook;
using Application.Book.Commands.UpdateBook;
using Application.DTOs.Book;

namespace BookRental.Web.Models;

public class BookViewModel
{
    public string? Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTimeOffset PublicationDate { get; set; }
    
    public List<string> GenreIds { get; set; } = new();
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
            GenreIds = dto.Genres.Select(g => g.Id).ToList(),
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
            GenreIds = GenreIds,
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
            GenreIds = GenreIds,
            AvailableQuantity = AvailableQuantity,
            RentalPrice = RentalPrice
        };
    }
}