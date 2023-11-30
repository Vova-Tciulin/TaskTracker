using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Groups;
using WebApp.Services;

namespace WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMapper _map;
    private readonly ILogger<HomeController> _logger;
    private readonly IGroupService _groupService;

    public HomeController(ILogger<HomeController> logger, IGroupService groupService, IMapper map)
    {
        _logger = logger;
        _groupService = groupService;
        _map = map;
    }

    public async Task<IActionResult> Index()
    {
        
       var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");

        var groupsDto = await _groupService.GetGroupsByUserId(userId.Value);
        var groupsVm = _map.Map<List<GroupVm>>(groupsDto);
        
        
        return View(groupsVm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}