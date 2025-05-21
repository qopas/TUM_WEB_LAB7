using Application.DTOs.Destination;
using Application.Mapping;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Commands.CreateDestination;

public class CreateDestinationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDestinationCommand, DestinationDto>
{
    public async Task<DestinationDto> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        var destination = Convert(request);

        var createdDestination = await unitOfWork.Destinations.AddAsync(destination);
        await unitOfWork.SaveChangesAsync();

        return DestinationDto.FromEntity(createdDestination);
    }

    private static BookRental.Domain.Entities.Destination Convert(CreateDestinationCommand request)
    {
        return new BookRental.Domain.Entities.Destination
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            ContactPerson = request.ContactPerson,
            PhoneNumber = request.PhoneNumber
        };
    }
}