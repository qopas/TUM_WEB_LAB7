namespace BookRental.Domain.Entities.Base;

public abstract class FullAuditableEntity : AuditableEntity, IFullAuditable
{
    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}