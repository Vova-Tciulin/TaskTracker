using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.User;

namespace WebApp.Controllers;

public class AccountController:Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [Authorize]
    public IActionResult Login()
    {
        _logger.LogInformation($"User is authenticated. UserName: {User.Identity.Name}");
        
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        await HttpContext.SignOutAsync("oidc");
    }

    [Authorize]
    [HttpPost]
    public IActionResult GetUserInfoView([FromBody] UserVm model)
    {
        return PartialView("UserInfoPartial", model);
    }
}