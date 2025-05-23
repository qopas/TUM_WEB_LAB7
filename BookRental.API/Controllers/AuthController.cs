using System.Security.Claims;
using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Logout;
using Application.Authentication.Commands.RefreshToken;
using Application.Authentication.Commands.Register;
using Application.DTOs.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController(IMediator mediator, ILogger<AuthController> logger) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(AuthResponseDto.CreateFailure([ex.Message]));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during registration");
            return BadRequest(AuthResponseDto.CreateFailure(["An error occurred during registration"]));
        }
    }
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(AuthResponseDto.CreateFailure([ex.Message]));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during login");
            return BadRequest(AuthResponseDto.CreateFailure(["An error occurred during login"]));
        }
    }
        
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(AuthResponseDto.CreateFailure([ex.Message]));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during token refresh");
            return BadRequest(AuthResponseDto.CreateFailure(["An error occurred during token refresh"]));
        }
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(new { message = "Logged out successfully" });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during logout");
            return BadRequest(new { message = "An error occurred during logout" });
        }
    }

    [HttpPost("logout-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LogoutAll(LogoutCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(new { message = "Logged out from all devices successfully" });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during logout all");
            return BadRequest(new { message = "An error occurred during logout" });
        }
    }
}