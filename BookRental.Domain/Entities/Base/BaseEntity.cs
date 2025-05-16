namespace BookRental.Domain.Entities.Base;

public class BaseEntity
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
}