namespace BookRental.Domain.Entities.Base;

public interface ITrackable
{
    string Id { get; set; }
    string CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
}