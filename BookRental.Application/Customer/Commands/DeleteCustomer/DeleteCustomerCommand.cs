using MediatR;

namespace Application.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<bool>
{
    public required string Id { get; init; }
}