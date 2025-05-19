using Application.DTOs.Book;
using MediatR;

namespace Application.Mediator.Book.Commands.CreateBook;

public class CreateBookCommand : IRequest<BookDto>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
}