using Application.DTOs.Customer;
using Application.Mediator.Customer.Commands.CreateCustomer;
using Application.Mediator.Customer.Commands.DeleteCustomer;
using Application.Mediator.Customer.Commands.UpdateCustomer;
using Application.Mediator.Customer.Queries.GetCustomerById;
using Application.Mediator.Customer.Queries.GetCustomers;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomers()
    {
        var query = new GetCustomersQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        var query = new GetCustomerByIdQuery { Id = id };
        var result = await mediator.Send(query);
        
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
    {
        var result = await mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Customer with id: {command.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var command = new DeleteCustomerCommand { Id = id };
        var result = await mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Customer with id: {id} deleted");
    }
}