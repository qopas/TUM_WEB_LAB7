using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.UpdateDestination;

public class UpdateDestinationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDestinationCommand, bool>
{
    public async Task<bool> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var existingDestination = await unitOfWork.Destinations.GetByIdAsync(request.Id);
        if (existingDestination == null)
        {
            return false;
        }

        existingDestination.Name = request.Name;
        existingDestination.Address = request.Address;
        existingDestination.City = request.City;
        existingDestination.ContactPerson = request.ContactPerson;
        existingDestination.PhoneNumber = request.PhoneNumber;

        await unitOfWork.Destinations.Update(existingDestination);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}