using MediatR;

namespace Application.Mediator.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommand : IRequest<bool>
{
    public string Id { get; set; }
}
