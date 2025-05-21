using BookRental.Domain.Enums;

namespace Application.DTOs.Rent;

public class RentDto
{
    public string Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public RentStatus Status { get; set; } 
    public string BookTitle { get; set; }
    public string CustomerName { get; set; }
    public string DestinationName { get; set; }
    public static RentDto FromEntity(BookRental.Domain.Entities.Rent rent)
    {
        return new RentDto
        {
            Id = rent.Id,
            BookId = rent.BookId,
            CustomerId = rent.CustomerId,
            DestinationId = rent.DestinationId,
            RentDate = rent.RentDate,
            DueDate = rent.DueDate,
            ReturnDate = rent.ReturnDate,
            Status = rent.Status,
            BookTitle = rent.Book?.Title,
            CustomerName = rent.Customer != null ? $"{rent.Customer.FirstName} {rent.Customer.LastName}" : null,
            DestinationName = rent.Destination?.Name
        };
    }
        
    public static IEnumerable<RentDto> FromEntityList(IEnumerable<BookRental.Domain.Entities.Rent> rents)
    {
        return rents.Select(FromEntity);
    }
}