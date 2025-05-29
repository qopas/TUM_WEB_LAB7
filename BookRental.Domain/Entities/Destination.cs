using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class Destination : FullAuditableEntity 
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string ContactPerson { get; private set; }
    public string PhoneNumber { get; private set; }
    public virtual ICollection<Rent> Rents { get; private set; } 

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

    public void Update(DestinationModel model, string updatedBy)
    {
        Name = model.Name;
        Address = model.Address;
        City = model.City;
        ContactPerson = model.ContactPerson;
        PhoneNumber = model.PhoneNumber;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void SoftDelete(string deletedBy)
    {
        IsDeleted = true;
        DeletedBy = deletedBy;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}
