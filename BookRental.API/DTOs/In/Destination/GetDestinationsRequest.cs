using Application.Destination.Queries.GetDestinations;

namespace BookRental.DTOs.In.Destination;

public class GetDestinationsRequest : IRequestIn<GetDestinationsQuery>
{
    public GetDestinationsQuery Convert()
    {
        return new GetDestinationsQuery();
    }
}