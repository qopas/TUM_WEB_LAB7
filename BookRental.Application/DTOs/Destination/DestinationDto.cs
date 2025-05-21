namespace Application.DTOs.Destination;

public class DestinationDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    
    public static DestinationDto FromEntity(BookRental.Domain.Entities.Destination destination)
    {
        return new DestinationDto
        {
            Id = destination.Id,
            Name = destination.Name,
            Address = destination.Address,
            City = destination.City,
            ContactPerson = destination.ContactPerson,
            PhoneNumber = destination.PhoneNumber
        };
    }
        
    public static IEnumerable<DestinationDto> FromEntityList(IEnumerable<BookRental.Domain.Entities.Destination> destinations)
    {
        return destinations.Select(FromEntity);
    }
}