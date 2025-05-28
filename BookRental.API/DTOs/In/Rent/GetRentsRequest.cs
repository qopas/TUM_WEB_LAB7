using Application.Rent.Queries.GetRents;

namespace BookRental.DTOs.In.Rent;

public class GetRentsRequest : IRequestIn<GetRentsQuery>
{
    public GetRentsQuery Convert()
    {
        return new GetRentsQuery();
    }
}
