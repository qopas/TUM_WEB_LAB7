using MediatR;

namespace Application.Mediator.Book.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
}