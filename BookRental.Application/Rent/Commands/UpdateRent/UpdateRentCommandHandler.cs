using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Rent.Commands.UpdateRent;

public class UpdateRentCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer) : IRequestHandler<UpdateRentCommand, bool>
{
    public async Task<bool> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var (existingRent, handle) = await Convert(request);
        if (!handle) return false;

        await unitOfWork.Rents.UpdateAsync(existingRent);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(BookRental.Domain.Entities.Rent? existingRent, bool handle)> Convert(UpdateRentCommand request)
    {
        var existingRent = await unitOfWork.Rents.GetByIdOrThrowAsync(request.Id, localizer);
        if (existingRent == null)
        {
            return (existingRent, false);
        }

        existingRent.BookId = request.BookId;
        existingRent.CustomerId = request.CustomerId;
        existingRent.DestinationId = request.DestinationId;
        existingRent.RentDate = request.RentDate;
        existingRent.DueDate = request.DueDate;
        if (request.ReturnDate != null) existingRent.ReturnDate = request.ReturnDate.Value;
        existingRent.Status = request.Status;
        return (existingRent, true);
    }
}