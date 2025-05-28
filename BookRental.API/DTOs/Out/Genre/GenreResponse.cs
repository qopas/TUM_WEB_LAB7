using Application.DTOs.Genre;

namespace BookRental.DTOs.Out.Genre;

public class GenreResponse : IResponseOut<GenreDto>
{
    public string Id { get; set; }
    public string Name { get; set; }

    public object Convert(GenreDto dto)
    {
        return new GenreResponse
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}
