using Application.Customer.Commands.DeleteCustomer;

namespace BookRental.DTOs.In.Customer;

public class DeleteCustomerRequest : IRequestIn<DeleteCustomerCommand>
{ 
    public string Id { get; set; }

    public DeleteCustomerCommand Convert()
    {
        return new DeleteCustomerCommand
        {
            Id = Id
        };
    }
}