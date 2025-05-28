using Microsoft.AspNetCore.Identity;

namespace BookRental.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }
    public string? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
}