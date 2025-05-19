using Application.DTOs.Destination;
using MediatR;

namespace Application.Mediator.Destination.Queries.GetDestinations;

public class GetDestinationsQuery : IRequest<IEnumerable<DestinationDto>>
{
   
}