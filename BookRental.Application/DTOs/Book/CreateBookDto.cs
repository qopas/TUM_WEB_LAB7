namespace Application.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTimeOffset PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; } 
    public decimal RentalPrice { get; set; }
}