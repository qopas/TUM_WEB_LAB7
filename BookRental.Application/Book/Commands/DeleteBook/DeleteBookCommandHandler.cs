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
        await unitOfWork.Books.SoftDeleteAsync(request.Id);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}