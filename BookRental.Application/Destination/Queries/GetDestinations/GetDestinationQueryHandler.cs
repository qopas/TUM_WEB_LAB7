using Application.DTOs.Destination;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Mediator.Destination.Queries.GetDestinations;

public class GetDestinationsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDestinationsQuery, IEnumerable<DestinationDto>>
{
    public Task<IEnumerable<DestinationDto>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        var destinations =  unitOfWork.Destinations.GetAll();
        return Task.FromResult(DestinationDto.FromEntityList(destinations));
    }
}