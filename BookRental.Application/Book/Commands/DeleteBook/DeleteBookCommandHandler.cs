using Application.Mediator.Book.Commands.DeleteBook;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookCommand, bool>
{
    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(request.Id);
        if (book == null)
        {
            return false;
        }

        await unitOfWork.Books.Delete(book);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}