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
    
}