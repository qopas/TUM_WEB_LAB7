using Application.DTOs.Destination;
using MediatR;

namespace Application.Destination.Queries.GetDestinationById;

public class GetDestinationByIdQuery : IRequest<DestinationDto>
{
    public required string Id { get; init; }
}
