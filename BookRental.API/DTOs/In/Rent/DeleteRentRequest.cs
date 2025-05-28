using Application.Rent.Commands.DeleteRent;

namespace BookRental.DTOs.In.Rent;

public class DeleteRentRequest : IRequestIn<DeleteRentCommand>
{
    public string Id { get; set; }

    public DeleteRentCommand Convert()
    {
        return new DeleteRentCommand
        {
            Id = Id
        };
    }
}
