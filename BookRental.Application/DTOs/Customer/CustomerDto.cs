namespace Application.DTOs.Customer;

public class CustomerDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    
    public static CustomerDto FromEntity(BookRental.Domain.Entities.Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            City = customer.City
        };
    }
        
    public static IEnumerable<CustomerDto> FromEntityList(IEnumerable<BookRental.Domain.Entities.Customer> customers)
    {
        return customers.Select(FromEntity);
    }
}