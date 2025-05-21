using Application.DTOs.Book;
using Application.Mediator.Book.Queries.GetBooks;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Book.Queries.GetBooks;

public class GetBooksQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = unitOfWork.Books.GetAll();
        return BookDto.FromEntityList(books);
    }
}