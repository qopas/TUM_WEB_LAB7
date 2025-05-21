using Application.DTOs.Book;
using MediatR;

namespace Application.Book.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public required string Id { get; init; }
}