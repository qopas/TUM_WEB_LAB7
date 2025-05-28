using Application.DTOs.Customer;

namespace BookRental.DTOs.Out.Customer;

public class CustomerResponse : IResponseOut<CustomerDto>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    public object Convert(CustomerDto dto)
    {
        return new CustomerResponse
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            City = dto.City
        };
    }
}