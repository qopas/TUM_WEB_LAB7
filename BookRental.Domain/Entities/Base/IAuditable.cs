using BookRental.Domain.Entities.Base;

public interface IAuditable : ITrackable
{
    string? UpdatedBy { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
}