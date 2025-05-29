using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class RefreshToken : FullAuditableEntity 
{
    public string Token { get; private set; }
    public string JwtId { get; private set; }
    public DateTimeOffset CreationDate { get; private set; }
    public DateTimeOffset ExpiryDate { get; private set; }
    public bool Used { get; set; }
    public bool Invalidated { get; private set; }
    
    public string UserId { get; private set; }
    public virtual ApplicationUser User { get; private set; }

    public static Result<RefreshToken> Create(RefreshTokenModel model)
    {
        var refreshToken = new RefreshToken
        {
            Token = model.Token,
            JwtId = model.JwtId,
            CreationDate = model.CreationDate,
            ExpiryDate = model.ExpiryDate,
            Used = model.Used,
            Invalidated = model.Invalidated,
            UserId = model.UserId
        };

        return Result<RefreshToken>.Success(refreshToken);
    }

    public void Update(RefreshTokenModel model, string updatedBy)
    {
        Token = model.Token;
        JwtId = model.JwtId;
        CreationDate = model.CreationDate;
        ExpiryDate = model.ExpiryDate;
        Used = model.Used;
        Invalidated = model.Invalidated;
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