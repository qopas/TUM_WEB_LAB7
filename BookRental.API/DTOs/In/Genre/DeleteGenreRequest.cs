using Application.Genres.Commands.DeleteGenre;

namespace BookRental.DTOs.In.Genre;

public class DeleteGenreRequest : IRequestIn<DeleteGenreCommand>
{
    public string Id { get; set; }

    public DeleteGenreCommand Convert()
    {
        return new DeleteGenreCommand
        {
            Id = Id
        };
    }
}
