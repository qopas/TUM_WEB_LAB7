using Application.Genres.Queries.GetGenreById;

namespace BookRental.DTOs.In.Genre;

public class GetGenreByIdRequest : IRequestIn<GetGenreByIdQuery>
{
    public string Id { get; set; }

    public GetGenreByIdQuery Convert()
    {
        return new GetGenreByIdQuery
        {
            Id = Id
        };
    }
}
