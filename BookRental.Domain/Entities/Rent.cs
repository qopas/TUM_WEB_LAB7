using BookRental.Domain.Enums;

namespace BookRental.Domain.Entities;

public class Rent
{
    public int RentId { get; set; }
    public int BookId { get; set; }
    public int CustomerId { get; set; }
    public int DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
    
    public Book Book { get; set; }
    public Customer Customer { get; set; }
    public Destination Destination { get; set; }
}