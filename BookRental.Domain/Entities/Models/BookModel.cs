namespace BookRental.Domain.Entities.Models;

public class BookModel
{
    public string Title { get; init; }
    public string Author { get; init; }
    public DateTimeOffset PublicationDate { get; init; }
    public int AvailableQuantity { get; init; }
    public decimal RentalPrice { get; init; }
    public IEnumerable<string> GenreIds { get; init; }
}

