using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace BookRental.Web.Models;

public class LoginViewModel
{
    public string Email { get; set; } = string.Empty;
    
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}