using BookRental.Domain.Entities.Base;

namespace BookRental.Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}