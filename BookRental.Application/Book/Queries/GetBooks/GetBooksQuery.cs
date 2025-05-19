using Application.DTOs.Book;
using MediatR;

namespace Application.Mediator.Book.Queries.GetBooks;

public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
{

}