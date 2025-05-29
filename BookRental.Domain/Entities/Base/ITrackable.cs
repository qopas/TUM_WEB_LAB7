namespace BookRental.Domain.Entities.Base;

public interface ITrackable
{
    string CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
}