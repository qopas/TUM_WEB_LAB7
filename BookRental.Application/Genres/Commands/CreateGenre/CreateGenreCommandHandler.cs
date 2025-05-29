using Application.DTOs.Genre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using MediatR;

namespace Application.Genres.Commands.CreateGenre;

public class CreateGenreCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateGenreCommand, Result<GenreDto>>
{
    public async Task<Result<GenreDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genreModel = new GenreModel
        {
            Name = request.Name
        };
        
        var genreResult = Genre.Create(genreModel);
        if (!genreResult.IsSuccess)
            return Result<GenreDto>.Failure(genreResult.Errors);

        var createdGenre = await unitOfWork.Genres.AddAsync(genreResult.Value);
        await unitOfWork.SaveChangesAsync();
        return Result<GenreDto>.Success(GenreDto.FromEntity(createdGenre));
    }
}
