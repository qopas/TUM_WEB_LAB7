using Application.Customer.Commands.CreateCustomer;
using Application.Customer.Commands.UpdateCustomer;
using Application.DTOs.Customer;

namespace BookRental.Web.Models;

public class CustomerViewModel
{
    public string? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    
    public static CustomerViewModel FromDto(CustomerDto dto)
    {
        return new CustomerViewModel
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            City = dto.City
        };
    }

    public CreateCustomerCommand ToCreateCommand()
    {
        return new CreateCustomerCommand
        {
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            City = City
        };
    }
    
    public UpdateCustomerCommand ToUpdateCommand()
    {
        return new UpdateCustomerCommand
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            City = City
        };
    }
}