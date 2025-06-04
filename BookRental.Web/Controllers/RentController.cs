using Application.Rent.Commands.CreateRent;
using Application.Rent.Commands.UpdateRent;
using Application.Rent.Commands.DeleteRent;
using Application.Rent.Queries.GetRentById;
using Microsoft.AspNetCore.Mvc;
using Application.Rent.Queries.GetRents;
using BookRental.Web.Models;
using MediatR;

namespace BookRental.Web.Controllers;

public class RentController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await ExecuteViewAsync(async () =>
        {
            var rents = await mediator.Send(new GetRentsQuery());
            return rents.Select(RentViewModel.FromDto);
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetRentsData()
    {
        return await ExecuteAsync(async () =>
        {
            var rents = await mediator.Send(new GetRentsQuery());
            return new { 
                data = rents.Select(RentViewModel.FromDto)
            };
        });
    }
    [HttpPost]
    public IActionResult GetModalBody([FromForm] RentViewModel? model = null)
    {
        var viewModel = model ?? new RentViewModel();
        return PartialView("_RentModalBody", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var rent = await mediator.Send(new GetRentByIdQuery { Id = id });
            return RentViewModel.FromDto(rent);
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(RentViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToCreateCommand())
        );
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(RentViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToUpdateCommand())
        );
    }
    
    [HttpPost]
    public async Task<IActionResult> Return(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var rent = await mediator.Send(new GetRentByIdQuery { Id = id });
            var rentViewModel = RentViewModel.FromDto(rent);
            
            rentViewModel.ReturnDate = DateTimeOffset.Now;
            rentViewModel.Status = BookRental.Domain.Enums.RentStatus.Returned;
            
            return await mediator.Send(rentViewModel.ToUpdateCommand());
        });
    }
}