using Application.Genres.Commands.UpdateGenre;

namespace BookRental.DTOs.In.Genre;

public class UpdateGenreRequest : IRequestIn<UpdateGenreCommand>
{
    public string Id { get; set; }
    public string Name { get; set; }

    public UpdateGenreCommand Convert()
    {
        return new UpdateGenreCommand
        {
            Id = Id,
            Name = Name
        };
    }
}
