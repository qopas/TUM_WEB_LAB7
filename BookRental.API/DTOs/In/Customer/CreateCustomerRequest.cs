using Application.Customer.Commands.CreateCustomer;

namespace BookRental.DTOs.In.Customer; 
public class CreateCustomerRequest : IRequestIn<CreateCustomerCommand>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    public CreateCustomerCommand Convert()
    {
        return new CreateCustomerCommand
        {
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            City = City
        };
    }
}