using Application.DTOs.Customer;
using MediatR;

namespace Application.Mediator.Customer.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<CustomerDto>
{
    public string Id { get; set; }
}
