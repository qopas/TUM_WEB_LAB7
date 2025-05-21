namespace Application.DTOs.Book;

public class BookDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    
    public static BookDto FromEntity(BookRental.Domain.Entities.Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublicationDate = book.PublicationDate,
            GenreId = book.GenreId,
            AvailableQuantity = book.AvailableQuantity,
            RentalPrice = book.RentalPrice
        };
    }
    
}