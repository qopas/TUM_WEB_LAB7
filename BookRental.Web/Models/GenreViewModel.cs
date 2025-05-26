using Application.DTOs.Genre;
using Application.Genres.Commands.CreateGenre;
using Application.Genres.Commands.UpdateGenre;

namespace BookRental.Web.Models;

public class GenreViewModel
{
    public string? Id { get; set; }
    public string Name { get; set; }
    
    public static GenreViewModel FromDto(GenreDto dto)
    {
        return new GenreViewModel
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }

    public CreateGenreCommand ToCreateCommand()
    {
        return new CreateGenreCommand
        {
            Name = Name
        };
    }
    
    public UpdateGenreCommand ToUpdateCommand()
    {
        return new UpdateGenreCommand
        {
            Id = Id,
            Name = Name
        };
    }
}