using Application.Customer.Commands.CreateCustomer;
using Application.Customer.Commands.DeleteCustomer;
using Application.Customer.Commands.UpdateCustomer;
using Application.Customer.Queries.GetCustomerById;
using Application.Customer.Queries.GetCustomers;
using Application.DTOs.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage customers in the rental system")]
public class CustomerController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all customers", Description = "Retrieve a list of all registered customers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomers()
    {
        return await ExecuteAsync(new GetCustomersQuery());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get customer by ID", Description = "Retrieve details of a specific customer using their unique identifier")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        return await ExecuteAsync(new GetCustomerByIdQuery { Id = id });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new customer", Description = "Register a new customer in the system")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update customer", Description = "Update details of an existing customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete customer", Description = "Remove a customer from the system using their ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        return await ExecuteAsync(new DeleteCustomerCommand { Id = id });
    }
}