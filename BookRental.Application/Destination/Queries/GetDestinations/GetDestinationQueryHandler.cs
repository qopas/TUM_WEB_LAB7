using Application.DTOs.Destination;
using Application.Mapping;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Queries.GetDestinations;

public class GetDestinationsQueryHandler(IRepository<BookRental.Domain.Entities.Destination> destinationRepository)
    : IRequestHandler<GetDestinationsQuery, IEnumerable<DestinationDto>>
{
    public async Task<IEnumerable<DestinationDto>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        var destinations = await destinationRepository.GetAllAsync();
        return destinations.ToDtoList();
    }
}