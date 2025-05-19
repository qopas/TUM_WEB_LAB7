using Application.DTOs.Genre;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Queries.GetGenres;

public class GetGenresQueryHandler(IRepository<Genre> genreRepository) : IRequestHandler<GetGenresQuery, IEnumerable<GenreDto>>
{
    public async Task<IEnumerable<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await genreRepository.GetAllAsync();
        return genres.ToDtoList();
    }
}