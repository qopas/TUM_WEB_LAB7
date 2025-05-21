using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Commands.UpdateRent;

public class UpdateRentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRentCommand, bool>
{
    public async Task<bool> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var (existingRent, handle) = await Convert(request);
        if (!handle) return false;

        await unitOfWork.Rents.Update(existingRent);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(BookRental.Domain.Entities.Rent? existingRent, bool handle)> Convert(UpdateRentCommand request)
    {
        var existingRent = await unitOfWork.Rents.GetByIdAsync(request.Id);
        if (existingRent == null)
        {
            return (existingRent, false);
        }

        existingRent.BookId = request.BookId;
        existingRent.CustomerId = request.CustomerId;
        existingRent.DestinationId = request.DestinationId;
        existingRent.RentDate = request.RentDate;
        existingRent.DueDate = request.DueDate;
        existingRent.ReturnDate = request.ReturnDate;
        existingRent.Status = request.Status;
        return (existingRent, true);
    }
}