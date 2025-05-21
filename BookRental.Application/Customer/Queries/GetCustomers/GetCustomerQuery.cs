using Application.DTOs.Customer;
using MediatR;

namespace Application.Customer.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
}