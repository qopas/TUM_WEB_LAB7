  using Application.DTOs.Authentication;
  using Microsoft.AspNetCore.Mvc;

  namespace BookRental.Controllers;

  public abstract class BaseApiController() : ControllerBase
  {
      protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> action)
      {
          try
          {
              var result = await action();
              return Ok(result);
          }
          catch (Exception ex)
          {
              return BadRequest(new { success = false, error =  ex.Message });
          }
      }
  }