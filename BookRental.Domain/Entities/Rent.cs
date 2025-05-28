using BookRental.Domain.Entities.Base;
using BookRental.Domain.Enums;

namespace BookRental.Domain.Entities;

public class Rent : BaseEntity
{
    public DateTimeOffset RentDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
    
    public string BookId { get; set; }
    public virtual Book Book { get; set; }
    public string CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public string DestinationId { get; set; }
    public virtual Destination Destination { get; set; }
}