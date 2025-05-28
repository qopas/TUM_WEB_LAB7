using Application.Authentication.Commands.Logout;

namespace BookRental.DTOs.In.Auth;

public class LogoutRequest : IRequestIn<LogoutCommand>
{
    public string UserId { get; set; }
    public string? RefreshToken { get; set; }

    public LogoutCommand Convert()
    {
        return new LogoutCommand
        {
            UserId = UserId,
            RefreshToken = RefreshToken
        };
    }
}
