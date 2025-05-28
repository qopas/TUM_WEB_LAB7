using Application.Destination.Queries.GetDestinationById;

namespace BookRental.DTOs.In.Destination;

public class GetDestinationByIdRequest : IRequestIn<GetDestinationByIdQuery>
{
    public string Id { get; set; }

    public GetDestinationByIdQuery Convert()
    {
        return new GetDestinationByIdQuery
        {
            Id = Id
        };
    }
}