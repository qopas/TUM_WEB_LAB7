using System.ComponentModel.DataAnnotations;

namespace BookRental.Domain.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; } 
    public decimal RentalPrice { get; set; }
    
    public Entities.Book ToEntity()
    {
        return new Entities.Book
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