namespace BookRental.Domain.Entities.Models;

public class AuthModel
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string UserId { get; set; }
    public string? CustomerId { get; set; }
}