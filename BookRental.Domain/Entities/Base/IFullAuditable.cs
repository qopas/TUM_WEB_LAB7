namespace BookRental.Domain.Entities.Base;

public interface IFullAuditable : IAuditable
{
    string? DeletedBy { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    bool IsDeleted { get; set; }
}