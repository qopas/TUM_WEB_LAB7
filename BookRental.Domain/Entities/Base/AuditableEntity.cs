namespace BookRental.Domain.Entities.Base;

public abstract class AuditableEntity : TrackableEntity, IAuditable
{
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
