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
public class DestinationController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DestinationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinations()
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetDestinationsQuery()));
       
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestination(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetDestinationByIdQuery { Id = id }));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateDestination([FromBody] CreateDestinationCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
       
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDestination([FromBody] UpdateDestinationCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
       
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDestination(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new DeleteDestinationCommand { Id = id }));
        
    }
}