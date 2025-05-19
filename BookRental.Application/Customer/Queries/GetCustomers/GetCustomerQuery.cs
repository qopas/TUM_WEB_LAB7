using Application.DTOs.Customer;
using MediatR;

namespace Application.Mediator.Customer.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
}