namespace BookRental.Domain.Entities.Models;

public class RefreshTokenModel
{
    public string Token { get; set; }
    public string JwtId { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public bool Used { get; set; }
    public bool Invalidated { get; set; }
    public string UserId { get; set; }
}
