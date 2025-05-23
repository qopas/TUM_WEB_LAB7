using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler(IUnitOfWork unitOfWork,IStringLocalizer localizer) : IRequestHandler<DeleteBookCommand, bool>
{
    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Books.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}