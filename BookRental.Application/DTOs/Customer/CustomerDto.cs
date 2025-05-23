namespace Application.DTOs.Customer;

public class CustomerDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    
    public static CustomerDto FromEntity(BookRental.Domain.Entities.Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customer.Address,
            City = customer.City
        };
    }
    
}