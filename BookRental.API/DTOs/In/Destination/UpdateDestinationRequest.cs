using Application.Destination.Commands.UpdateDestination;

namespace BookRental.DTOs.In.Destination;

public class UpdateDestinationRequest : IRequestIn<UpdateDestinationCommand>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }

    public UpdateDestinationCommand Convert()
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