namespace BookRental.Domain.Entities.Models;

public class BookModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTimeOffset PublicationDate { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    public string GenreId { get; set; }
}
