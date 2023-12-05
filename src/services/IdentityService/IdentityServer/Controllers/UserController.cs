using System.Formats.Asn1;
using AutoMapper;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer)]
public class UserController:ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _map;

    public UserController(UserManager<User> userManager, IMapper map, ILogger<UserController> logger)
    {
        _userManager = userManager;
        _map = map;
        _logger = logger;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetUserInfoById(Guid userId)
    {
        _logger.LogInformation("Invoke GetUserInfoById");
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            return NotFound();
        }

        var userDto = _map.Map<UserDto>(user);
        return Ok(userDto);
    }
}