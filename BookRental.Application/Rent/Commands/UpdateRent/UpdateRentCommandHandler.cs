using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;

namespace Application.Rent.Commands.UpdateRent;

public class UpdateRentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRentCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await unitOfWork.Rents.UpdateAsync(request.Id, setters => setters
            .SetProperty(r => r.BookId, request.BookId)
            .SetProperty(r => r.CustomerId, request.CustomerId)
            .SetProperty(r => r.DestinationId, request.DestinationId)
            .SetProperty(r => r.RentDate, request.RentDate)
            .SetProperty(r => r.DueDate, request.DueDate)
            .SetProperty(r => r.Status, request.Status)
            .SetProperty(r => r.ReturnDate, request.ReturnDate));
        
        return rowsAffected.ToUpdateResult<BookRental.Domain.Entities.Rent>(request.Id);
    }
}