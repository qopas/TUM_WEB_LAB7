using Application.Destination.Commands.DeleteDestination;

namespace BookRental.DTOs.In.Destination;

public class DeleteDestinationRequest : IRequestIn<DeleteDestinationCommand>
{
    public string Id { get; set; }

    public DeleteDestinationCommand Convert()
    {
        return new DeleteDestinationCommand
        {
            Id = Id
        };
    }
}