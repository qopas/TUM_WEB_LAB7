using MediatR;

namespace Application.Mediator.Rent.Commands.DeleteRent;

public class DeleteRentCommand : IRequest<bool>
{
    public string Id { get; set; }
}