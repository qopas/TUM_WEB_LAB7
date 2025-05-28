using Application.Rent.Commands.CreateRent;

namespace BookRental.DTOs.In.Rent;

public class CreateRentRequest : IRequestIn<CreateRentCommand>
{
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTimeOffset RentDate { get; set; }
    public DateTimeOffset DueDate { get; set; }

    public CreateRentCommand Convert()
    {
        return new CreateRentCommand
        {
            BookId = BookId,
            CustomerId = CustomerId,
            DestinationId = DestinationId,
            RentDate = RentDate,
            DueDate = DueDate
        };
    }
}
