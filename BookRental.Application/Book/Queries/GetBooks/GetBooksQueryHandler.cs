using Application.DTOs.Book;
using Application.Mapping;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Book.Queries.GetBooks;

public class GetBooksQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetAllAsync();
        return books.ToDtoList();
    }
}