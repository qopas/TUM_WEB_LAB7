using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MediatR;

namespace BookRental.Web.Controllers;

[Authorize]
public abstract class BaseWebController : Controller
{
    protected async Task<IActionResult> ExecuteJsonAsync<T>(Func<Task<T>> action)
    {
        try
        {
            var result = await action();
            return Json(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> action)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            var result = await action();
            return View(result);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }
    protected async Task<IActionResult> ExecuteAsync(Func<Task> action, string redirectAction = "Index", string? redirectController = null)
    {
        if (!ModelState.IsValid)
            return View();
        try
        {
            await action();
            return RedirectToAction(redirectAction, redirectController);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    protected async Task<IActionResult> ExecuteWithResultAsync(Func<Task<IActionResult>> action)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    protected async Task SignInUserAsync(string userId, string token, string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Email, email),
            new("JwtToken", token)
        };

        var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = false,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    protected async Task SignOutUserAsync()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    }
}