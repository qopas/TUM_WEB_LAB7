using Application.Destination.Commands.CreateDestination;

namespace BookRental.DTOs.In.Destination;

public class CreateDestinationRequest : IRequestIn<CreateDestinationCommand>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }

    public CreateDestinationCommand Convert()
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
}