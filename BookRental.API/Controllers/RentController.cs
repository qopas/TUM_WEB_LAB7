using Application.DTOs.Rent;
using Application.Mapping;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentController(IRepository<Rent> rentRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<RentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRents()
    {
        var rents = await rentRepository.GetAllAsync();
        return Ok(rents.ToDtoList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRent(string id)
    {
        var rent = await rentRepository.GetByIdAsync(id);
        if (rent == null)
        {
            return NotFound();
        }
        return Ok(rent.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateRent([FromBody] CreateRentDto createRent)
    {
        var rent = createRent.ToEntity();
        var createdRent = await rentRepository.AddAsync(rent);
        return Ok(createdRent.ToDto());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentDto updateRent)
    {
        var existingRent = await rentRepository.GetByIdAsync(updateRent.Id);
        if (existingRent == null)
        {
            return NotFound();
        }
        await rentRepository.UpdateAsync(updateRent.ToEntity(existingRent));
        return Ok($"Rent with id: {updateRent.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        var rent = await rentRepository.GetByIdAsync(id);
        if (rent == null)
        {
            return NotFound();
        }
        await rentRepository.DeleteAsync(id);
        return Ok($"Rent with id: {id} deleted");
    }
    
    [HttpPut("{id}/return")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReturnBook(string id)
    {
        var rent = await rentRepository.GetByIdAsync(id);
        if (rent == null)
        {
            return NotFound();
        }
        
        rent.ReturnDate = DateTime.UtcNow;
        rent.Status = Domain.Enums.RentStatus.Returned;
        
        await rentRepository.UpdateAsync(rent);
        return Ok($"Book for rent with id: {id} was successfully returned");
    }
}