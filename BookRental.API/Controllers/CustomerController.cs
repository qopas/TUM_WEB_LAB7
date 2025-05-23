using Application.Customer.Commands.CreateCustomer;
using Application.Customer.Commands.DeleteCustomer;
using Application.Customer.Commands.UpdateCustomer;
using Application.Customer.Queries.GetCustomerById;
using Application.Customer.Queries.GetCustomers;
using Application.DTOs.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(IMediator mediator):BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomers()
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetCustomersQuery()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetCustomerByIdQuery { Id = id }));
      
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
       
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new DeleteCustomerCommand { Id = id }));
    }
}