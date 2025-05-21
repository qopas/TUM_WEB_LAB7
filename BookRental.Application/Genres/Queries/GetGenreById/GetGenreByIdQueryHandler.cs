using Application.DTOs.Genre;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Genres.Queries.GetGenreById;

public class GetGenreByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGenreByIdQuery, GenreDto>
{
    public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(request.Id);
        return GenreDto.FromEntity(genre);
    }
}