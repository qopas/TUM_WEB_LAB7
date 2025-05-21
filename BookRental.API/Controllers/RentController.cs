using Application.DTOs.Rent;
using Application.Rent.Commands.CreateRent;
using Application.Rent.Commands.DeleteRent;
using Application.Rent.Commands.UpdateRent;
using Application.Rent.Queries.GetRentById;
using Application.Rent.Queries.GetRents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRents()
    {
        try
        {
            var query = new GetRentsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRent(string id)
    {
        try
        {
            var query = new GetRentByIdQuery { Id = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRent([FromBody] CreateRentCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok($"Rent with id: {command.Id} was successfully updated");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        try
        {
            var command = new DeleteRentCommand { Id = id };
            var result = await mediator.Send(command);
            return Ok($"Rent with id: {id} deleted");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}