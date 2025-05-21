using Application.DTOs.Destination;
using MediatR;

namespace Application.Destination.Queries.GetDestinations;

public class GetDestinationsQuery : IRequest<IEnumerable<DestinationDto>>
{
   
}