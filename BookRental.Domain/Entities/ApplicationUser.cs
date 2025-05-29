using Microsoft.AspNetCore.Identity;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }
    public string? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public static Result<ApplicationUser> Create(ApplicationUserModel model)
    {
        var user = new ApplicationUser
        {
            CreatedAt = model.CreatedAt,
            LastLoginAt = model.LastLoginAt,
            CustomerId = model.CustomerId
        };

        return Result<ApplicationUser>.Success(user);
    }
}
