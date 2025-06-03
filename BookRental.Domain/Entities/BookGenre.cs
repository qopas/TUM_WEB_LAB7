using BookRental.Domain.Entities.Base;

namespace BookRental.Domain.Entities;

public class BookGenre: FullAuditableEntity
{
    public string BookId { get; private set; }
    public virtual Book Book { get; private set; }
    
    public string GenreId { get; private set; }
    public virtual Genre Genre { get; private set; }
    

    public static BookGenre Create(string bookId, string genreId)
    {
        return new BookGenre
        {
            BookId = bookId,
            GenreId = genreId
        };
    }
}