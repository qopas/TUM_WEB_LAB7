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
    
    public virtual ICollection<Genre> Genres { get; private set; } 
    public virtual ICollection<Rent> Rents { get; private set; } 

    public static Result<Book> Create(BookModel model)
    {
        var book = new Book
        {
            Title = model.Title,
            Author = model.Author,
            PublicationDate = model.PublicationDate,
            AvailableQuantity = model.AvailableQuantity,
            RentalPrice = model.RentalPrice,
            Genres = []
        };

        return Result<Book>.Success(book);
    }
}