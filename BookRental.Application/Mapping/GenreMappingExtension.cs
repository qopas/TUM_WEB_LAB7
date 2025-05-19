using Application.DTOs.Genre;
using BookRental.Domain.Entities;

namespace Application.Mapping;

public static class GenreMappingExtensions
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public static IEnumerable<GenreDto> ToDtoList(this IEnumerable<Genre> genres)
    {
        return genres.Select(genre => genre.ToDto());
    }

    public static Genre ToEntity(this CreateGenreDto createGenreDto)
    {
        return new Genre
        {
            Name = createGenreDto.Name
        };
    }

    public static Genre ToEntity(this UpdateGenreDto updateGenreDto, Genre existingGenre)
    {
        existingGenre.Name = updateGenreDto.Name;
        return existingGenre;
    }
}