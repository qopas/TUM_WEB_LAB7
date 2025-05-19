using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommandHandler(IRepository<BookRental.Domain.Entities.Destination> destinationRepository)
    : IRequestHandler<DeleteDestinationCommand, bool>
{
    public async Task<bool> Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.Id);
        if (destination == null)
        {
            return false;
        }

        await destinationRepository.DeleteAsync(request.Id);
        return true;
    }
}