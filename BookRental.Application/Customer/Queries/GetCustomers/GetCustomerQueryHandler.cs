using Application.DTOs.Customer;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Customer.Queries.GetCustomers;

public class GetCustomersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
{
    public Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers =  unitOfWork.Customers.GetAll();
        return Task.FromResult(customers.Select(CustomerDto.FromEntity));
    }
}