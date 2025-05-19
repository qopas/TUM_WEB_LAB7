using Application.DTOs.Customer;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(IRepository<Customer> customerRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await customerRepository.GetAllAsync();
        return Ok(customers.ToDtoList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomer)
    {
        var customer = createCustomer.ToEntity();
        var createdCustomer = await customerRepository.AddAsync(customer);
        return Ok(createdCustomer.ToDto());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomer)
    {
        var existingCustomer = await customerRepository.GetByIdAsync(updateCustomer.Id);
        if (existingCustomer == null)
        {
            return NotFound();
        }

        await customerRepository.UpdateAsync(updateCustomer.ToEntity(existingCustomer));
        return Ok($"Customer with id: {updateCustomer.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        await customerRepository.DeleteAsync(id);
        return Ok($"Customer with id: {id} deleted");
    }
}