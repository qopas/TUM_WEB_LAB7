using Application.DTOs.Book;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Book.Queries.GetBookById;

public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer) : IRequestHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdOrThrowAsync(request.Id, localizer);
        return BookDto.FromEntity(book);
    }
}