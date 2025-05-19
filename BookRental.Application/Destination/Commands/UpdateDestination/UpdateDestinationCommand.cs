using MediatR;

namespace Application.Mediator.Destination.Commands.UpdateDestination;

public class UpdateDestinationCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
}