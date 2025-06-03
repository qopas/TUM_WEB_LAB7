using BookRental.Domain.Common;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler(IUnitOfWork unitOfWork,IStringLocalizer localizer) : IRequestHandler<DeleteBookCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdOrThrowAsync(request.Id);
        foreach (var bookGenre in book.BookGenres) 
        {
            await unitOfWork.BookGenre.SoftDeleteAsync(bookGenre.Id);
        }
        await unitOfWork.Books.SoftDeleteAsync(request.Id);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}