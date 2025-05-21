using MediatR;

namespace Application.Rent.Commands.DeleteRent;

public class DeleteRentCommand : IRequest<bool>
{
    public required string Id { get; init; }
}