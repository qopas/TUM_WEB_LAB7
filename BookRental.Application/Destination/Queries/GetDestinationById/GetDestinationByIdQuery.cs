using Application.DTOs.Destination;
using MediatR;

namespace Application.Mediator.Destination.Queries.GetDestinationById;

public class GetDestinationByIdQuery : IRequest<DestinationDto>
{
    public string Id { get; set; }
}
