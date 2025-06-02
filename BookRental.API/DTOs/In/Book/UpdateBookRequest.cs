using Application.Book.Commands.UpdateBook;

namespace BookRental.DTOs.In.Book;

public class UpdateBookRequest : IRequestIn<UpdateBookCommand>
{
    public string Id { get; set; } 
    public string Title { get; set; }
    public string Author { get; set; } 
    public DateTimeOffset PublicationDate { get; set; }
    public IEnumerable<string> GenreIds { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }

    public UpdateBookCommand Convert()
    {
        return new UpdateBookCommand
        {
            Id = Id,
            Title = Title,
            Author = Author,
            PublicationDate = PublicationDate,
            GenreIds = GenreIds,
            AvailableQuantity = AvailableQuantity,
            RentalPrice = RentalPrice
        };
    }
}