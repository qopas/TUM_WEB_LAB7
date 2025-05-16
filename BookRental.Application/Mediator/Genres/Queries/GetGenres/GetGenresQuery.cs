using BookRental.Domain.DTOs.Genre;
using MediatR;

namespace Application.Mediator.Genres.Queries.GetGenres;

public class GetGenresQuery : IRequest<List<GenreDto>>
{
}
