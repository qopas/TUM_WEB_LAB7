using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Commands.UpdateRent;

public class UpdateRentCommandHandler(IRepository<BookRental.Domain.Entities.Rent> rentRepository) : IRequestHandler<UpdateRentCommand, bool>
{
    public async Task<bool> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var existingRent = await rentRepository.GetByIdAsync(request.Id);
        if (existingRent == null)
        {
            return false;
        }

        existingRent.BookId = request.BookId;
        existingRent.CustomerId = request.CustomerId;
        existingRent.DestinationId = request.DestinationId;
        existingRent.RentDate = request.RentDate;
        existingRent.DueDate = request.DueDate;
        existingRent.ReturnDate = request.ReturnDate;
        existingRent.Status = request.Status;

        await rentRepository.UpdateAsync(existingRent);
        return true;
    }
}