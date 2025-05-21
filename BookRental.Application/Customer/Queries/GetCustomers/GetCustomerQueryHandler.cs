using Application.DTOs.Customer;
using Application.Mapping;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Queries.GetCustomers;

public class GetCustomersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers =  unitOfWork.Customers.GetAll();
        return  customers.ToDtoList();
    }
}