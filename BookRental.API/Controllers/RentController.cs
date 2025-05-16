

using Application.Mapping;
using BookRental.Domain.DTOs.Rent;
using BookRental.Domain.Entities;
using BookRental.Domain.Enums;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentController(BookRentalDbContext dbContext) : ControllerBase
{
    
    [HttpGet]
    [ProducesResponseType(typeof(List<RentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRents()
    {
        var rents = await dbContext.Set<Rent>()
            .Include(r => r.Book)
            .Include(r => r.Customer)
            .Include(r => r.Destination)
            .ToListAsync();
            
        return Ok(rents.ToDtoList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRent(string id)
    {
        var rent = await dbContext.Set<Rent>()
            .Include(r => r.Book)
            .Include(r => r.Customer)
            .Include(r => r.Destination)
            .FirstOrDefaultAsync(r => r.Id == id);
            
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
        
        await dbContext.Set<Rent>().AddAsync(rent);
        await dbContext.SaveChangesAsync();
        
        var createdRent = await dbContext.Set<Rent>()
            .Include(r => r.Book)
            .Include(r => r.Customer)
            .Include(r => r.Destination)
            .FirstOrDefaultAsync(r => r.Id == rent.Id);
            
        return Ok(createdRent.ToDto());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentDto updateRent)
    {
        var existingRent = await dbContext.Set<Rent>()
            .FirstOrDefaultAsync(r => r.Id == updateRent.Id);
            
        if (existingRent == null)
        {
            return NotFound();
        }
        
   
        existingRent.BookId = updateRent.BookId;
        existingRent.CustomerId = updateRent.CustomerId;
        existingRent.DestinationId = updateRent.DestinationId;
        existingRent.RentDate = updateRent.RentDate;
        existingRent.DueDate = updateRent.DueDate;
        existingRent.ReturnDate = updateRent.ReturnDate;
        existingRent.Status = updateRent.Status;
        
        dbContext.Set<Rent>().Update(existingRent);
        await dbContext.SaveChangesAsync();
        
        return Ok($"Rent with id: {updateRent.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        var rent = await dbContext.Set<Rent>()
            .FirstOrDefaultAsync(r => r.Id == id);
            
        if (rent == null)
        {
            return NotFound();
        }
        
        dbContext.Set<Rent>().Remove(rent);
        await dbContext.SaveChangesAsync();
        
        return Ok($"Rent with id: {id} deleted");
    }
    
    [HttpPut("{id}/return")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReturnBook(string id)
    {
        var rent = await dbContext.Set<Rent>()
            .FirstOrDefaultAsync(r => r.Id == id);
            
        if (rent == null)
        {
            return NotFound();
        }
        
        rent.ReturnDate = DateTime.UtcNow;
        rent.Status = RentStatus.Returned;
        
        dbContext.Set<Rent>().Update(rent);
        await dbContext.SaveChangesAsync();
        
        return Ok($"Book for rent with id: {id} was successfully returned");
    }
}