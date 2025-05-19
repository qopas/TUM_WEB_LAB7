using Application.DTOs.Book;
using MediatR;

namespace Application.Mediator.Book.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public string Id { get; set; }
}