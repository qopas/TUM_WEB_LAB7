using Application.DTOs.Genre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Genres.Commands.CreateGenre;

public class CreateGenreCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateGenreCommand, GenreDto>
{
    public async Task<GenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = Convert(request);
        var createdGendre = await unitOfWork.Genres.AddAsync(genre);
        await unitOfWork.SaveChangesAsync();
        return GenreDto.FromEntity(createdGendre);
    }

    private static Genre Convert(CreateGenreCommand request)
    {
        var genre = new Genre()
        {
            Name = request.Name
        };
        return genre;
    }
}