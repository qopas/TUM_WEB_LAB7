
using Application.DTOs.Genre;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Commands.CreateGenre;

public class CreateGenreCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateGenreCommand, GenreDto>
{
    public async Task<GenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre()
        {
            Name = request.Name
        };
        var createdGendre = await unitOfWork.Genres.AddAsync(genre);
        await unitOfWork.SaveChangesAsync();
        return createdGendre.ToDto();
    }
}