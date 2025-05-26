using Application.Destination.Commands.CreateDestination;
using Application.Destination.Commands.UpdateDestination;
using Application.DTOs.Destination;

namespace BookRental.Web.Models;

public class DestinationViewModel
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    
    public static DestinationViewModel FromDto(DestinationDto dto)
    {
        return new DestinationViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            ContactPerson = dto.ContactPerson,
            PhoneNumber = dto.PhoneNumber
        };
    }

    public CreateDestinationCommand ToCreateCommand()
    {
        return new CreateDestinationCommand
        {
            Name = Name,
            Address = Address,
            City = City,
            ContactPerson = ContactPerson,
            PhoneNumber = PhoneNumber
        };
    }
    
    public UpdateDestinationCommand ToUpdateCommand()
    {
        return new UpdateDestinationCommand
        {
            Id = Id,
            Name = Name,
            Address = Address,
            City = City,
            ContactPerson = ContactPerson,
            PhoneNumber = PhoneNumber
        };
    }
}