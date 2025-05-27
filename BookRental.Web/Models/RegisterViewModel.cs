using Application.Authentication.Commands.Register;
using FluentValidation;

namespace BookRental.Web.Models;

public class RegisterViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }

    public RegisterCommand ConvertToRegisterCommand()
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