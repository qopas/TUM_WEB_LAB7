using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<UpdateBookCommand, bool>
{
    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.Id);
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

        await bookRepository.UpdateAsync(book);
        return true;
    }
}