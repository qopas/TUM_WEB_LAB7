using Application.Mediator.Book.Commands.UpdateBook;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookCommand, bool>
{
    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(request.Id);
        if (book == null)
        {
            return false;
        }
        book.Title = request.Title;
        book.Author = request.Author;
        book.PublicationDate = request.PublicationDate;
        book.GenreId = request.GenreId;
        book.AvailableQuantity = request.AvailableQuantity;
        book.RentalPrice = request.RentalPrice;

        await unitOfWork.Books.Update(book);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}