namespace BookRental.Domain.Entities.Base;

public abstract class TrackableEntity : ITrackable
{
    public string Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    protected TrackableEntity()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTimeOffset.Now;
    }
}