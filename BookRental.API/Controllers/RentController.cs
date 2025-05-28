using Application.DTOs.Rent;
using Application.Rent.Commands.CreateRent;
using Application.Rent.Commands.DeleteRent;
using Application.Rent.Commands.UpdateRent;
using Application.Rent.Queries.GetRentById;
using Application.Rent.Queries.GetRents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage book rental transactions")]
public class RentController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all rentals", Description = "Retrieve a list of all book rental transactions")]
    [ProducesResponseType(typeof(IEnumerable<RentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRents()
    {
        return await ExecuteAsync(new GetRentsQuery());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get rental by ID", Description = "Retrieve details of a specific rental transaction using its unique identifier")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRent(string id)
    {
        return await ExecuteAsync(new GetRentByIdQuery { Id = id });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new rental", Description = "Create a new book rental transaction")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRent([FromBody] CreateRentCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update rental", Description = "Update details of an existing rental transaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete rental", Description = "Remove a rental transaction from the system using its ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        return await ExecuteAsync(new DeleteRentCommand { Id = id });
    }
}