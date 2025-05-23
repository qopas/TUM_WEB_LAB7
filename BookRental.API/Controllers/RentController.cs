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
public class RentController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRents()
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetRentsQuery()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRent(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetRentByIdQuery { Id = id }));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRent([FromBody] CreateRentCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new DeleteRentCommand { Id = id }));
    }
}