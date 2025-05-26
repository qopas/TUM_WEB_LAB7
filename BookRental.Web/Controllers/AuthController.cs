using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Register;
using BookRental.Web.Models;
using MediatR;

namespace BookRental.Web.Controllers;

[AllowAnonymous]
public class AuthController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return await ExecuteWithResultAsync(async () =>
        {
            var command = new LoginCommand { Email = model.Email, Password = model.Password };
            var result = await mediator.Send(command);
            await SignInUserAsync(result.UserId, result.Token, model.Email);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        });
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        return await ExecuteAsync(async () =>
        {
            var command = new RegisterCommand
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                PhoneNumber = model.PhoneNumber
            };

            var result = await mediator.Send(command);
            await SignInUserAsync(result.UserId, result.Token, model.Email);
        }, "Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await SignOutUserAsync();
        return RedirectToAction("Index", "Home");
    }
}