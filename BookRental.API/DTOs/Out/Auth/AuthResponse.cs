using Application.DTOs.Authentication;

namespace BookRental.DTOs.Out.Auth;

public class AuthResponse : IResponseOut<AuthResponseDto>
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? UserId { get; set; }
    public string? CustomerId { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public object Convert(AuthResponseDto dto)
    {
        return new AuthResponse
        {
            Success = dto.Success,
            Token = dto.Token,
            RefreshToken = dto.RefreshToken,
            UserId = dto.UserId,
            CustomerId = dto.CustomerId,
            Errors = dto.Errors
        };
    }
}
