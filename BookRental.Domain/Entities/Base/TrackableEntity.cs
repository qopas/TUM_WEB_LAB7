namespace BookRental.Domain.Entities.Base;

public abstract class TrackableEntity : ITrackable
{
    public string Id { get; protected set; } = Guid.NewGuid().ToString();
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    protected TrackableEntity()
    {
        CreatedAt = DateTimeOffset.Now;
    }
}