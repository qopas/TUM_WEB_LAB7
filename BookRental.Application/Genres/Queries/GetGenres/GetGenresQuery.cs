using Application.DTOs.Genre;
using MediatR;

namespace Application.Genres.Queries.GetGenres;

public class GetGenresQuery : IRequest<List<GenreDto>>
{
}
