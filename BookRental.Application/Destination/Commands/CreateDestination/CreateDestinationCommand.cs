using Application.DTOs.Destination;
using MediatR;

namespace Application.Mediator.Destination.Commands.CreateDestination;

public class CreateDestinationCommand : IRequest<DestinationDto>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
}
