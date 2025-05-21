using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.UpdateDestination;

public class UpdateDestinationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDestinationCommand, bool>
{
    public async Task<bool> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var (existingDestination, handle) = await Convert(request);
        if (!handle) return false;

        await unitOfWork.Destinations.Update(existingDestination);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(BookRental.Domain.Entities.Destination? existingDestination, bool handle)> Convert(UpdateDestinationCommand request)
    {
        var existingDestination = await unitOfWork.Destinations.GetByIdAsync(request.Id);
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