using Application.Authentication.Commands.Login;

namespace BookRental.DTOs.In.Auth;

public class LoginRequest : IRequestIn<LoginCommand>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginCommand Convert()
    {
        return new LoginCommand
        {
            Email = Email,
            Password = Password
        };
    }
}
