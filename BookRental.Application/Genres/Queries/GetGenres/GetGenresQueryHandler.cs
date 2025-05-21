using Application.DTOs.Genre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Queries.GetGenres;

public class GetGenresQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGenresQuery, IEnumerable<GenreDto>>
{
    public Task<IEnumerable<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var genres =  unitOfWork.Genres.GetAll();
        return Task.FromResult(GenreDto.FromEntityList(genres));
    }
}