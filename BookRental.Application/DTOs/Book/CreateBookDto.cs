namespace Application.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; } 
    public decimal RentalPrice { get; set; }
    
    public BookRental.Domain.Entities.Book ToEntity()
    {
        return new BookRental.Domain.Entities.Book
        {
            Title = Title,
            Author = Author,
            PublicationDate = PublicationDate,
            GenreId = GenreId,
            AvailableQuantity = AvailableQuantity,
            RentalPrice = RentalPrice
        };
    }
}