using Application.DTOs.Customer;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Customer.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.Id);
        return CustomerDto.FromEntity(customer);
    }
}