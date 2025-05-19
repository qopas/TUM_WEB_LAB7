using Application.DTOs.Customer;
using BookRental.Domain.Entities;

namespace Application.Mapping;

public static class CustomerMappingExtensions
{
    public static CustomerDto ToDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            City = customer.City
        };
    }

    public static IEnumerable<CustomerDto> ToDtoList(this IEnumerable<Customer> customers)
    {
        return customers.Select(customer => customer.ToDto());
    }

    public static Customer ToEntity(this CreateCustomerDto createCustomerDto)
    {
        return new Customer
        {
            FirstName = createCustomerDto.FirstName,
            LastName = createCustomerDto.LastName,
            Email = createCustomerDto.Email,
            PhoneNumber = createCustomerDto.PhoneNumber,
            Address = createCustomerDto.Address,
            City = createCustomerDto.City
        };
    }

    public static Customer ToEntity(this UpdateCustomerDto updateCustomerDto, Customer existingCustomer)
    {
        existingCustomer.FirstName = updateCustomerDto.FirstName;
        existingCustomer.LastName = updateCustomerDto.LastName;
        existingCustomer.Email = updateCustomerDto.Email;
        existingCustomer.PhoneNumber = updateCustomerDto.PhoneNumber;
        existingCustomer.Address = updateCustomerDto.Address;
        existingCustomer.City = updateCustomerDto.City;
        
        return existingCustomer;
    }
}