using Application.Rent.Commands.UpdateRent;
using BookRental.Domain.Enums;

namespace BookRental.DTOs.In.Rent;

public class UpdateRentRequest : IRequestIn<UpdateRentCommand>
{
    public string Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTimeOffset RentDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public RentStatus Status { get; set; }

    public UpdateRentCommand Convert()
    {
        return new UpdateRentCommand
        {
            Id = Id,
            BookId = BookId,
            CustomerId = CustomerId,
            DestinationId = DestinationId,
            RentDate = RentDate,
            DueDate = DueDate,
            ReturnDate = ReturnDate,
            Status = Status
        };
    }
}
