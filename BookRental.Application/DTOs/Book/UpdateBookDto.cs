namespace Application.DTOs.Book;

public class UpdateBookDto
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string? GenreId { get; set; }
    public int? AvailableQuantity { get; set; }
    public decimal? RentalPrice { get; set; }
    public static BookRental.Domain.Entities.Book ToEntity(UpdateBookDto dto, BookRental.Domain.Entities.Book existingBook)
    {
        if (dto.Title != null)
            existingBook.Title = dto.Title;
                
        if (dto.Author != null)
            existingBook.Author = dto.Author;
                
        if (dto.PublicationDate.HasValue)
            existingBook.PublicationDate = dto.PublicationDate.Value;
                
        if (dto.GenreId != null)
            existingBook.GenreId = dto.GenreId;
                
        if (dto.AvailableQuantity.HasValue)
            existingBook.AvailableQuantity = dto.AvailableQuantity.Value;
                
        if (dto.RentalPrice.HasValue)
            existingBook.RentalPrice = dto.RentalPrice.Value;
                
        return existingBook;
    }
}