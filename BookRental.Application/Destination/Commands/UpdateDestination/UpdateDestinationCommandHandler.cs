using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;

namespace Application.Destination.Commands.UpdateDestination;

public class UpdateDestinationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateDestinationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await unitOfWork.Destinations.UpdateAsync(request.Id, setters => setters
            .SetProperty(d => d.Name, request.Name)
            .SetProperty(d => d.Address, request.Address)
            .SetProperty(d => d.City, request.City)
            .SetProperty(d => d.ContactPerson, request.ContactPerson)
            .SetProperty(d => d.PhoneNumber, request.PhoneNumber));
        
        return rowsAffected.ToUpdateResult<BookRental.Domain.Entities.Destination>(request.Id);
    }
}
