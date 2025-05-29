using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;

namespace Application.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await unitOfWork.Books.UpdateAsync(request.Id, setters => setters
            .SetProperty(b => b.Title, request.Title)
            .SetProperty(b => b.Author, request.Author)
            .SetProperty(b => b.PublicationDate, request.PublicationDate)
            .SetProperty(b => b.GenreId, request.GenreId)
            .SetProperty(b => b.AvailableQuantity, request.AvailableQuantity)
            .SetProperty(b => b.RentalPrice, request.RentalPrice));
        
        return rowsAffected.ToUpdateResult<BookRental.Domain.Entities.Book>(request.Id);
    }
}