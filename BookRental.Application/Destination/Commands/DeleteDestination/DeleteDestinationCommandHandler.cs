using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;

namespace Application.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDestinationCommand, bool>
{
    public async Task<bool> Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Destinations.DeleteOrThrowAsync(request.Id);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}