using Application.Authentication.Commands.Register;

namespace BookRental.DTOs.In.Auth;

public class RegisterRequest : IRequestIn<RegisterCommand>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }

    public RegisterCommand Convert()
    {
        return new RegisterCommand
        {
            Email = Email,
            Password = Password,
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            City = City,
            PhoneNumber = PhoneNumber
        };
    }
}
