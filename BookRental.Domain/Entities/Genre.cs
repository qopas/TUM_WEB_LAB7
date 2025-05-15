namespace BookRental.Domain.Entities;

public class Genre
{
    public Genre()
    {
        Books = new HashSet<Book>();
    }
    public int GenreId { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}