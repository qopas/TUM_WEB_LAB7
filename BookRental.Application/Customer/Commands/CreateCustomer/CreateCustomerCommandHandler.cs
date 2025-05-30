using Application.DTOs.Customer;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using MediatR;

namespace Application.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerModel = new CustomerModel
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            City = request.City
        };

        var customerResult = BookRental.Domain.Entities.Customer.Create(customerModel);
        if (!customerResult.IsSuccess)
            return Result<CustomerDto>.Failure(customerResult.Errors);

        var createdCustomer = await unitOfWork.Customers.CreateAsync(customerResult.Value);
        await unitOfWork.SaveChangesAsync();
        return Result<CustomerDto>.Success(CustomerDto.FromEntity(createdCustomer));
    }
}
