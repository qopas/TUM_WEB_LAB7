using Application.DTOs.Book;
using MediatR;

namespace Application.Book.Queries.GetBooks;

public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
{

}