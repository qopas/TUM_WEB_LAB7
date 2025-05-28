  using Application.DTOs.Authentication;
  using MediatR;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  namespace BookRental.Controllers;
  
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public abstract class BaseApiController(IMediator mediator) : ControllerBase
  {
      protected async Task<IActionResult> ExecuteAsync<T>(IRequest<T> request)
      {
          try
          {
              var result = await mediator.Send(request);
              return Ok(result);
          }
          catch (Exception ex)
          {
              return BadRequest(new { success = false, error = ex.Message });
          }
      }
  }