using Application.DTOs.Destination;

namespace BookRental.DTOs.Out.Destination;

public class DestinationResponse : IResponseOut<DestinationDto>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }

    public object Convert(DestinationDto dto)
    {
        return new DestinationResponse
        {
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            ContactPerson = dto.ContactPerson,
            PhoneNumber = dto.PhoneNumber
        };
    }
}