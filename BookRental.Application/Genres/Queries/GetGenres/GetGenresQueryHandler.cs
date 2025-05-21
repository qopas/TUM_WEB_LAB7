using Application.DTOs.Genre;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Genres.Queries.GetGenres;

public class GetGenresQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGenresQuery, IEnumerable<GenreDto>>
{
    public Task<IEnumerable<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var genres =  unitOfWork.Genres.GetAll();
        return Task.FromResult(GenreDto.FromEntityList(genres));
    }
}