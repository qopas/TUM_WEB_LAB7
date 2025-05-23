using Microsoft.AspNetCore.Identity;

namespace BookRental.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
}