using Application.DTOs.Genre;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Queries.GetGenreById;

public class GetGenreByIdQueryHandler(IRepository<Genre> genreRepository) : IRequestHandler<GetGenreByIdQuery, GenreDto>
{
    public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await genreRepository.GetByIdAsync(request.Id);
        return genre?.ToDto();
    }
}