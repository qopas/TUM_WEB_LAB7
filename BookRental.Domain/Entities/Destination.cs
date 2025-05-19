using BookRental.Domain.Entities.Base;

namespace BookRental.Domain.Entities;

public class Destination : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ICollection<Rent> Rents { get; set; }
    
}