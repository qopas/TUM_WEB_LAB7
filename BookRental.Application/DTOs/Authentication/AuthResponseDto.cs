namespace Application.DTOs.Authentication;

public class AuthResponseDto
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? UserId { get; set; }
    public string? CustomerId { get; set; }
    public IEnumerable<string> Errors { get; set; }
    
    public static AuthResponseDto CreateSuccess(string token, string refreshToken, string userId, string? customerId)
    {
        return new AuthResponseDto
        {
            Success = true,
            Token = token,
            RefreshToken = refreshToken,
            UserId = userId,
            CustomerId = customerId
        };
    }

    public static AuthResponseDto CreateFailure(IEnumerable<string> errors)
    {
        return new AuthResponseDto
        {
            Success = false,
            Errors = errors
        };
    }
    
}