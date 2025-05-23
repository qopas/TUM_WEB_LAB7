using Application.DTOs.Customer;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Customer.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdOrThrowAsync(request.Id, localizer);
        return CustomerDto.FromEntity(customer);
    }
}