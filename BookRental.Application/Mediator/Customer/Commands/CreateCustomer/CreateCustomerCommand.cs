using Application.DTOs.Customer;
using MediatR;

namespace Application.Mediator.Customer.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CustomerDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
}