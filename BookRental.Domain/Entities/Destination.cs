namespace BookRental.Domain.Entities;

public class Destination
{
    public int DestinationId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
}