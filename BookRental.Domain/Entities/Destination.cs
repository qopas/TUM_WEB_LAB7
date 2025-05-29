using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class Destination : BaseEntity
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string ContactPerson { get; private set; }
    public string PhoneNumber { get; private set; }
    public virtual ICollection<Rent> Rents { get; private set; } = new HashSet<Rent>();

    public static Result<Destination> Create(DestinationModel model)
    {
        var destination = new Destination
        {
            Name = model.Name,
            Address = model.Address,
            City = model.City,
            ContactPerson = model.ContactPerson,
            PhoneNumber = model.PhoneNumber
        };

        return Result<Destination>.Success(destination);
    }
}
