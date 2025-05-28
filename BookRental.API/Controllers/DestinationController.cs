using Application.Destination.Commands.CreateDestination;
using Application.Destination.Commands.DeleteDestination;
using Application.Destination.Commands.UpdateDestination;
using Application.Destination.Queries.GetDestinationById;
using Application.Destination.Queries.GetDestinations;
using Application.DTOs.Destination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage rental destinations")]
public class DestinationController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all destinations", Description = "Retrieve a list of all available rental destinations")]
    [ProducesResponseType(typeof(IEnumerable<DestinationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinations()
    {
        return await ExecuteAsync(new GetDestinationsQuery());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get destination by ID", Description = "Retrieve details of a specific destination using its unique identifier")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestination(string id)
    {
        return await ExecuteAsync(new GetDestinationByIdQuery { Id = id });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new destination", Description = "Add a new rental destination to the system")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateDestination([FromBody] CreateDestinationCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update destination", Description = "Update details of an existing destination")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDestination([FromBody] UpdateDestinationCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete destination", Description = "Remove a destination from the system using its ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDestination(string id)
    {
        return await ExecuteAsync(new DeleteDestinationCommand { Id = id });
    }
}