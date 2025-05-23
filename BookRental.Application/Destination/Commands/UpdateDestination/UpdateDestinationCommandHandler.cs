using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Destination.Commands.UpdateDestination;

public class UpdateDestinationCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer)
    : IRequestHandler<UpdateDestinationCommand, bool>
{
    public async Task<bool> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var (existingDestination, handle) = await Convert(request);
        if (!handle) return false;

        await unitOfWork.Destinations.UpdateAsync(existingDestination);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(BookRental.Domain.Entities.Destination? existingDestination, bool handle)> Convert(UpdateDestinationCommand request)
    {
        var existingDestination = await unitOfWork.Destinations.GetByIdOrThrowAsync(request.Id, localizer);
        if (existingDestination == null)
        {
            return (existingDestination, false);
        }

        existingDestination.Name = request.Name;
        existingDestination.Address = request.Address;
        existingDestination.City = request.City;
        existingDestination.ContactPerson = request.ContactPerson;
        existingDestination.PhoneNumber = request.PhoneNumber;
        return (existingDestination, true);
    }
}