using Application.Rent.Queries.GetRentById;

namespace BookRental.DTOs.In.Rent;

public class GetRentByIdRequest : IRequestIn<GetRentByIdQuery>
{
    public string Id { get; set; }

    public GetRentByIdQuery Convert()
    {
        return new GetRentByIdQuery
        {
            Id = Id
        };
    }
}
