namespace Application.DTOs.Destination;

public class CreateDestinationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public static BookRental.Domain.Entities.Destination ToEntity(CreateDestinationDto dto)
    {
        return new BookRental.Domain.Entities.Destination
        {
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            ContactPerson = dto.ContactPerson,
            PhoneNumber = dto.PhoneNumber
        };
    }
}