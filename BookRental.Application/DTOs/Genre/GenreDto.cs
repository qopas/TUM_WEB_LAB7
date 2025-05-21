namespace Application.DTOs.Genre;

public class GenreDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public static GenreDto FromEntity(BookRental.Domain.Entities.Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
        
    public static IEnumerable<GenreDto> FromEntityList(IEnumerable<BookRental.Domain.Entities.Genre> genres)
    {
        return genres.Select(FromEntity);
    }
}