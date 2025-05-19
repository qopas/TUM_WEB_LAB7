
using Application.DTOs.Destination;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinationController(IRepository<Destination> destinationRepository) : ControllerBase
{


    [HttpGet]
    [ProducesResponseType(typeof(List<DestinationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDestinations()
    {
        var destinations = await destinationRepository.GetAllAsync();
        return Ok(destinations.ToDtoList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDestination(string id)
    {
        var destination = await destinationRepository.GetByIdAsync(id);
        if (destination == null)
        {
            return NotFound();
        }
        return Ok(destination.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDestination([FromBody] CreateDestinationDto createDestination)
    {
        var destination = createDestination.ToEntity();
        var createdDestination = await destinationRepository.AddAsync(destination);
        return Ok(createdDestination.ToDto());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDestination([FromBody] UpdateDestinationDto updateDestination)
    {
        var existingDestination = await destinationRepository.GetByIdAsync(updateDestination.Id);
        if (existingDestination == null)
        {
            return NotFound();
        }
        await destinationRepository.UpdateAsync(updateDestination.ToEntity(existingDestination));
        return Ok($"Destination with id: {updateDestination.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDestination(string id)
    {
        var destination = await destinationRepository.GetByIdAsync(id);
        if (destination == null)
        {
            return NotFound();
        }
        await destinationRepository.DeleteAsync(id);
        return Ok($"Destination with id: {id} deleted");
    }
}