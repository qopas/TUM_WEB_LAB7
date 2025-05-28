using Application.DTOs.Book;
using BookRental.DTOs.Out;

namespace BookRental.DTOs.Out.Book;

public class BookOutResponse : IResponseOut<BookDto>
{
    public string Id { get; set; } 
    public string Title { get; set; }
    public string Author { get; set; } 
    public DateTimeOffset PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }

    public object Convert(BookDto dto)
    {
        return new BookOutResponse
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
}