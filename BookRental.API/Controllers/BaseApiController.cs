using Application.Exceptions;
using BookRental.Domain.Common;
using BookRental.DTOs.Out;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace BookRental.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController(IMediator mediator) : ControllerBase
{
    protected async Task<IActionResult> ExecuteAsync<TOutResponse, TResult>(IBaseRequest request)
        where TOutResponse : IResponseOut<TResult>, new()
    {
        var result = await mediator.Send(request);

        var response = new BaseResponse<object>
        {
            Success = true,
            Data = result switch
            {
                Result<TResult> { IsSuccess: false } resultObj => HandleFailureResult(resultObj),
                Result<TResult> resultObj => new TOutResponse().Convert(resultObj.Value),
                TResult dto => new TOutResponse().Convert(dto),
                _ => result
            }
        };

        return Ok(response);
    }

    private static object HandleFailureResult<TResult>(Result<TResult> result)
    {
        if (result.Errors?.Any() == true)
        {
            throw new BusinessLogicException(result.Errors);
        }

        throw new BusinessLogicException("An unknown error occurred");
    }
}