using Application.Destination.Commands.CreateDestination;
using Application.Destination.Commands.DeleteDestination;
using Application.Destination.Commands.UpdateDestination;
using Application.Destination.Queries.GetDestinationById;
using Application.Destination.Queries.GetDestinations;
using Application.DTOs.Destination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinationController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DestinationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinations()
    {
        try
        {
            var query = new GetDestinationsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestination(string id)
    {
        try
        {
            var query = new GetDestinationByIdQuery { Id = id };
            var result = await mediator.Send(query);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateDestination([FromBody] CreateDestinationCommand command)
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
    public async Task<IActionResult> UpdateDestination([FromBody] UpdateDestinationCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok($"Destination with id: {command.Id} was successfully updated");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDestination(string id)
    {
        try
        {
            var command = new DeleteDestinationCommand { Id = id };
            var result = await mediator.Send(command);
            return Ok($"Destination with id: {id} deleted");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}