using System.Text.RegularExpressions;
using Application.DTOs.Customer;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Customer.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CustomerDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
}
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email address is required")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(new Regex(@"^\+?[0-9\s-()]{7,20}$")).WithMessage("A valid phone number is required")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");

        RuleFor(c => c.Address)
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(c => c.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters");
    }
}