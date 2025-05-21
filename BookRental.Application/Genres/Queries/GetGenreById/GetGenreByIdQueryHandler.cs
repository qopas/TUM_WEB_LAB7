using Application.DTOs.Genre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Queries.GetGenreById;

public class GetGenreByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGenreByIdQuery, GenreDto>
{
    public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(request.Id);
        return GenreDto.FromEntity(genre);
    }
}