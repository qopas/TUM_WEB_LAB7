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
    
    public string GenreId { get; private set; }
    public virtual Genre Genre { get; private set; }
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
            GenreId = model.GenreId
        };

        return Result<Book>.Success(book);
    }

    public void Update(BookModel model, string updatedBy)
    {
        Title = model.Title;
        Author = model.Author;
        PublicationDate = model.PublicationDate;
        AvailableQuantity = model.AvailableQuantity;
        RentalPrice = model.RentalPrice;
        GenreId = model.GenreId;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void SoftDelete(string deletedBy)
    {
        IsDeleted = true;
        DeletedBy = deletedBy;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}