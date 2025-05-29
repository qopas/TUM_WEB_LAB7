using System.Text.RegularExpressions;
using Application.DTOs.Customer;
using BookRental.Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Customer.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<Result<CustomerDto>>
{
    public required string FirstName { get; set; }
    public required string LastName { get; init; }
    public required string Address { get; init; }
    public required string City { get; init; }
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
        
        RuleFor(c => c.Address)
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(c => c.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters");
    }
}