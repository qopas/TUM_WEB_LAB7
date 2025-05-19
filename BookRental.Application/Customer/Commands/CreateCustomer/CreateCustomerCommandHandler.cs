using Application.DTOs.Customer;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(IRepository<BookRental.Domain.Entities.Customer> customerRepository)
    : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
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

        var createdCustomer = await customerRepository.AddAsync(customer);
        return createdCustomer.ToDto();
    }
}