using Application.Book.Commands.DeleteBook;
using MediatR;

namespace BookRental.DTOs.In.Book;

public class DeleteBookRequest : IRequestIn<DeleteBookCommand>
{
    public string Id { get; set; }

    public DeleteBookCommand Convert()
    {
        return new DeleteBookCommand { Id = Id };
    }
}