using Application.Genres.Commands.CreateGenre;

namespace BookRental.DTOs.In.Genre;

public class CreateGenreRequest : IRequestIn<CreateGenreCommand>
{
    public string Name { get; set; }

    public CreateGenreCommand Convert()
    {
        return new CreateGenreCommand
        {
            Name = Name
        };
    }
}
