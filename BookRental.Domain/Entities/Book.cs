using BookRental.Domain.Entities.Base;

namespace BookRental.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string GenreId { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal RentalPrice { get; set; }
    
    public virtual Genre Genre { get; private set; }
    public virtual ICollection<Rent> Rents { get; private set; }
}