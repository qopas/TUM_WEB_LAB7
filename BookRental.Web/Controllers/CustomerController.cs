using Application.Customer.Commands.DeleteCustomer;
using Application.Customer.Queries.GetCustomerById;
using Microsoft.AspNetCore.Mvc;
using Application.Customer.Queries.GetCustomers;
using BookRental.Web.Models;
using MediatR;

namespace BookRental.Web.Controllers;

public class CustomerController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await ExecuteViewAsync(async () =>
        {
            var customers = await mediator.Send(new GetCustomersQuery());
            return customers.Select(CustomerViewModel.FromDto);
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomersData()
    {
        return await ExecuteAsync(async () =>
        {
            var customers = await mediator.Send(new GetCustomersQuery());
            return new { 
                data = customers.Select(CustomerViewModel.FromDto)
            };
        });
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var customer = await mediator.Send(new GetCustomerByIdQuery { Id = id });
            return CustomerViewModel.FromDto(customer);
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CustomerViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToCreateCommand())
        );
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(CustomerViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToUpdateCommand())
        );
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(new DeleteCustomerCommand { Id = id })
        );
    }
}