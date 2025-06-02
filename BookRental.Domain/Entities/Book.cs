using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class Book : FullAuditableEntity  
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public DateTimeOffset PublicationDate { get; private set; }
    public int AvailableQuantity { get; private set; }
    public decimal RentalPrice { get; private set; }
    
    public virtual ICollection<BookGenre> BookGenres { get; private set; }
    public virtual ICollection<Rent> Rents { get; private set; }

    public static Result<Book> Create(BookModel model)
    {
        var book = new Book
        {
            Title = model.Title,
            Author = model.Author,
            PublicationDate = model.PublicationDate,
            AvailableQuantity = model.AvailableQuantity,
            RentalPrice = model.RentalPrice
        };

        return Result<Book>.Success(book);
    }

    public void AddGenre(string genreId)
    {
        if (BookGenres.Any(bg => bg.GenreId == genreId)) return;
        var bookGenre = BookGenre.Create(Id, genreId);
        BookGenres.Add(bookGenre);
    }

    public void RemoveGenre(string genreId)
    {
        var bookGenre = BookGenres.FirstOrDefault(bg => bg.GenreId == genreId);
        if (bookGenre != null)
        {
            BookGenres.Remove(bookGenre);
        }
    }
}