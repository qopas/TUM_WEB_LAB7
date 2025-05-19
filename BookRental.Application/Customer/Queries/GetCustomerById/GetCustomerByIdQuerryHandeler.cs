using Application.DTOs.Customer;
using Application.Mapping;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(IRepository<BookRental.Domain.Entities.Customer> customerRepository)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id);
        return customer?.ToDto();
    }
}