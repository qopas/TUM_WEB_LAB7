using Application.Authentication.Commands.RefreshToken;

namespace BookRental.DTOs.In.Auth;

public class RefreshTokenRequest : IRequestIn<RefreshTokenCommand>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public RefreshTokenCommand Convert()
    {
        return new RefreshTokenCommand
        {
            Token = Token,
            RefreshToken = RefreshToken
        };
    }
}
