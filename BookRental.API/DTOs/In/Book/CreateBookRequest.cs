using Application.Book.Commands.CreateBook;

namespace BookRental.DTOs.In.Book;

public class CreateBookRequest : IRequestIn<CreateBookCommand>
{
    public string Title { get; set; }
    public string Author { get; set; } 
    public DateTimeOffset PublicationDate { get; set; }
    public IEnumerable<string> GenreIds { get; set; } 
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    
    public CreateBookCommand Convert()
    {
        return new CreateBookCommand
        {
            Title = Title,
            Author = Author,
            PublicationDate = PublicationDate,
            GenreIds = GenreIds,
            AvailableQuantity = AvailableQuantity,
            RentalPrice = RentalPrice
        };
    }
}