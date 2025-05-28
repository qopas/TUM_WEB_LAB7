using Application.Genres.Queries.GetGenres;

namespace BookRental.DTOs.In.Genre;

public class GetGenresRequest : IRequestIn<GetGenresQuery>
{
    public GetGenresQuery Convert()
    {
        return new GetGenresQuery();
    }
}
