using Application.DTOs.Destination;
using Application.Mapping;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.CreateDestination;

public class CreateDestinationCommandHandler(IRepository<BookRental.Domain.Entities.Destination> destinationRepository)
    : IRequestHandler<CreateDestinationCommand, DestinationDto>
{

    public async Task<DestinationDto> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        var destination = new BookRental.Domain.Entities.Destination
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            ContactPerson = request.ContactPerson,
            PhoneNumber = request.PhoneNumber
        };

        var createdDestination = await destinationRepository.AddAsync(destination);
        return createdDestination.ToDto();
    }
}