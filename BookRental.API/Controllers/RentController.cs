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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRents()
    {
        var query = new GetRentsQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRent(string id)
    {
        var query = new GetRentByIdQuery { Id = id };
        var result = await mediator.Send(query);
        
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateRent([FromBody] CreateRentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentCommand command)
    {
        var result = await mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Rent with id: {command.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        var command = new DeleteRentCommand { Id = id };
        var result = await mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Rent with id: {id} deleted");
    }
    
}