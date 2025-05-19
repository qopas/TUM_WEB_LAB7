using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.UpdateDestination;

public class UpdateDestinationCommandHandler(IRepository<BookRental.Domain.Entities.Destination> destinationRepository)
    : IRequestHandler<UpdateDestinationCommand, bool>
{
    public async Task<bool> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var existingDestination = await destinationRepository.GetByIdAsync(request.Id);
        if (existingDestination == null)
        {
            return false;
        }

        existingDestination.Name = request.Name;
        existingDestination.Address = request.Address;
        existingDestination.City = request.City;
        existingDestination.ContactPerson = request.ContactPerson;
        existingDestination.PhoneNumber = request.PhoneNumber;

        await destinationRepository.UpdateAsync(existingDestination);
        return true;
    }
}