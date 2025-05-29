namespace BookRental.Domain.Entities.Models;

public class ApplicationUserModel
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }
    public string? CustomerId { get; set; }
}
