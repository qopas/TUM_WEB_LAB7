using Application.DTOs.Customer;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Convert(request);

        var createdCustomer = await unitOfWork.Customers.AddAsync(customer);
        await unitOfWork.SaveChangesAsync();
        return CustomerDto.FromEntity(createdCustomer);
    }

    private static BookRental.Domain.Entities.Customer Convert(CreateCustomerCommand request)
    {
        var customer = new BookRental.Domain.Entities.Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            City = request.City
        };
        return customer;
    }
}