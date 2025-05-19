using Application.DTOs.Customer;
using Application.Mapping;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Queries.GetCustomers;

public class GetCustomersQueryHandler(IRepository<BookRental.Domain.Entities.Customer> customerRepository)
    : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await customerRepository.GetAllAsync();
        return customers.ToDtoList();
    }
}