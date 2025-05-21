using Application.DTOs.Destination;
using Application.Mapping;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Queries.GetDestinations;

public class GetDestinationsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDestinationsQuery, IEnumerable<DestinationDto>>
{
    public async Task<IEnumerable<DestinationDto>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        var destinations =  unitOfWork.Destinations.GetAll();
        return destinations.ToDtoList();
    }
}