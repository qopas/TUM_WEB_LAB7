using Application.DTOs.Book;
using Application.Mapping;
using Application.Mediator.Book.Queries.GetBookById;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Book.Queries.GetBookById;

public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(request.Id);
        return book?.ToDto();
    }
}