using Application.Customer.Commands.UpdateCustomer;

namespace BookRental.DTOs.In.Customer;

public class UpdateCustomerRequest : IRequestIn<UpdateCustomerCommand>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    public UpdateCustomerCommand Convert()
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