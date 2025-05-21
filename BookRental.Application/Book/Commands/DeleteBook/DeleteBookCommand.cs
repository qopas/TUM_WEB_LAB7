using MediatR;

namespace Application.Book.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<bool>
{
    public required string Id { get; init; }
}
