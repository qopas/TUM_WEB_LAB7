using BookRental.Domain.Enums;

namespace BookRental.Domain.Entities.Models;

public class RentModel
{
    public DateTimeOffset RentDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
}
