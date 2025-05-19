using MediatR;

namespace Application.Mediator.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<bool>
{
    public string Id { get; set; }
}