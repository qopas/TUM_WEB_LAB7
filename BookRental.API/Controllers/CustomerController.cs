using System.Reflection;
using Application.DTOs.Customer;
using Application.Customer.Queries.GetCustomers;
using Application.Customer.Queries.GetCustomerById;
using Application.Customer.Commands.CreateCustomer;
using Application.Customer.Commands.UpdateCustomer;
using Application.Customer.Commands.DeleteCustomer;
using BookRental.DTOs.In.Customer;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Customer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage customers in the rental system")]
public class CustomerController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all customers")]
    [ProducesResponseType(typeof(BaseEnumerableResponse<CustomerResponse, CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomers()
    {
        return await ExecuteAsync<BaseEnumerableResponse<CustomerResponse, CustomerDto>, IEnumerable<CustomerDto>>(new GetCustomersRequest().Convert());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get customer by ID")]
    [ProducesResponseType(typeof(BaseResponse<CustomerResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        return await ExecuteAsync<CustomerResponse, CustomerDto>(new GetCustomerByIdRequest { Id = id }.Convert());
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new customer")]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        return await ExecuteAsync<CustomerResponse, CustomerDto>(request.Convert());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update customer")]
    [ProducesResponseType(typeof(BaseResponse<CustomerUpdateResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest request)
    {
        return await ExecuteAsync<CustomerUpdateResponse, bool>(request.Convert());
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete customer")]
    [ProducesResponseType(typeof(BaseResponse<CustomerDeleteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        return await ExecuteAsync<CustomerDeleteResponse, bool>(new DeleteCustomerRequest { Id = id }.Convert());
    }
}