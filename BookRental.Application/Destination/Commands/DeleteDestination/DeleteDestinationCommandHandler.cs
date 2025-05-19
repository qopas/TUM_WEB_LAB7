using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDestinationCommand, bool>
{
    public async Task<bool> Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
    {
        var destination = await unitOfWork.Destinations.GetByIdAsync(request.Id);
        if (destination == null)
        {
            return false;
        }

        await unitOfWork.Destinations.DeleteAsync(request.Id);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}