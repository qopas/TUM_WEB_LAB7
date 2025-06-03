using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class Genre : TrackableEntity
{
    public string Name { get; private set; }
    public virtual ICollection<BookGenre> BookGenres { get; private set; }

    public static Result<Genre> Create(GenreModel model)
    {
        var genre = new Genre
        {
            Name = model.Name
        };

        return Result<Genre>.Success(genre);
    }
}