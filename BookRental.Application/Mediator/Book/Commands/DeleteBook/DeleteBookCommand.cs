using MediatR;

namespace Application.Mediator.Book.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<bool>
{
    public string Id { get; set; }
}
