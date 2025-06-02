using Application.DTOs.Book;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Genre;

namespace BookRental.DTOs.Out.Book;

public class BookResponse : IResponseOut<BookDto>
{
    public string Id { get; set; } 
    public string Title { get; set; }
    public string Author { get; set; } 
    public DateTimeOffset PublicationDate { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }

    public object Convert(BookDto dto)
    {
        return new BookResponse
        {
            Id = dto.Id,
            Title = dto.Title,
            Author = dto.Author,
            PublicationDate = dto.PublicationDate,
            Genres = dto.Genres.Select(g => (GenreResponse)new GenreResponse().Convert(g)),
            AvailableQuantity = dto.AvailableQuantity,
            RentalPrice = dto.RentalPrice
        };
    }
}