namespace BookRental.Domain.Entities;

public class Book
{
        
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public int GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    
    public Genre Genre { get; set; }
}