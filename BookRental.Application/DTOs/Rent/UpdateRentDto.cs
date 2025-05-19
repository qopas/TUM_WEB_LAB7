using BookRental.Domain.Enums;

namespace Application.DTOs.Rent;

public class UpdateRentDto
{
    public string Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
}