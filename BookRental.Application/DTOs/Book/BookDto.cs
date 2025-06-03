using Application.DTOs.Genre;

namespace Application.DTOs.Book;

public class BookDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTimeOffset PublicationDate { get; set; }
    public IEnumerable<GenreDto> Genres { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public static BookDto FromEntity(BookRental.Domain.Entities.Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublicationDate = book.PublicationDate,
            Genres = book.BookGenres.Select(bg => GenreDto.FromEntity(bg.Genre)),
            AvailableQuantity = book.AvailableQuantity,
            RentalPrice = book.RentalPrice,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}