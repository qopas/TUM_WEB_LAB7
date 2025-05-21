using Application.DTOs.Customer;
using MediatR;

namespace Application.Customer.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<CustomerDto>
{
    public required string Id { get; init; }
}
