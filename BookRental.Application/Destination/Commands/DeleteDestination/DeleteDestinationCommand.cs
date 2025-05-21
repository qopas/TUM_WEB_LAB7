using MediatR;

namespace Application.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommand : IRequest<bool>
{
    public required string Id { get; init; }
}
