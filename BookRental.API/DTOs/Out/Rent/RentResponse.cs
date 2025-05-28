using Application.DTOs.Rent;
using BookRental.Domain.Enums;

namespace BookRental.DTOs.Out.Rent;

public class RentResponse : IResponseOut<RentDto>
{
    public string Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTimeOffset RentDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
    public string BookTitle { get; set; }
    public string CustomerName { get; set; }
    public string DestinationName { get; set; }

    public object Convert(RentDto dto)
    {
        return new RentResponse
        {
            Id = dto.Id,
            BookId = dto.BookId,
            CustomerId = dto.CustomerId,
            DestinationId = dto.DestinationId,
            RentDate = dto.RentDate,
            DueDate = dto.DueDate,
            ReturnDate = dto.ReturnDate,
            Status = dto.Status,
            BookTitle = dto.BookTitle,
            CustomerName = dto.CustomerName,
            DestinationName = dto.DestinationName
        };
    }
}
