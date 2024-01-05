using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Groups;
using WebApp.Services;
using WebApp.Services.Group;
using WebApp.Services.ModelDto.Group;

namespace WebApp.Controllers;

[Authorize]
public class GroupController:Controller
{
    private readonly IMapper _map;
    private readonly ILogger<HomeController> _logger;
    private readonly IGroupService _groupService;

    public GroupController(IMapper map, ILogger<HomeController> logger, IGroupService groupService)
    {
        _map = map;
        _logger = logger;
        _groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroup(string groupId)
    {
        _logger.LogInformation($"invoke getGroup with id: {groupId}");
        var groupDto = await _groupService.GetGroupAggregatorById(groupId);
        
        var groupVm = _map.Map<GroupAggregatorVm>(groupDto);
        
        return PartialView("GroupPartial", groupVm);
    }

    [HttpGet]
    public IActionResult CreateGroup()
    {
        var createGroupVm = new CreateGroupVm();

        return PartialView("CreateGroupPartial", createGroupVm);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody]CreateGroupVm model)
    {
        _logger.LogInformation($"model: {JsonSerializer.Serialize(model)}");
        
        var newGroup= await _groupService.CreateGroup(new CreateGroupDto()
        {
            Description = model.Description
        });
        
        return Ok(newGroup);
    }
    
    public async Task<IActionResult> RemoveGroup(Guid groupId)
    {
        _logger.LogInformation($"remove group with id: {groupId}");
        await _groupService.RemoveGroup(groupId);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> LeaveTheGroup(Guid groupId)
    {
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");

        await _groupService.RemoveUserFromGroup(new RemoveUserFromGroupDto()
        {
            UserId = userId.Value,
            GroupId = groupId.ToString()
        });

        return RedirectToAction("Index", "Home");
        
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveUser(string groupId, string userId)
    {
        await _groupService.RemoveUserFromGroup(new RemoveUserFromGroupDto()
        {
            AuthorId = User.Claims.FirstOrDefault(u => u.Type == "sub").Value,
            GroupId = groupId,
            UserId = userId
        });
        
        return RedirectToAction("GetGroup", "Group", new { groupId });
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(string groupId, string user)
    {
        await _groupService.AddUserToGroup(new AddUserToGroupDto()
        {
            AuthorId = User.Claims.FirstOrDefault(u => u.Type == "sub").Value,
            NickNameOrEmail = user,
            GroupId = groupId
        });
        
        return RedirectToAction("GetGroup", "Group", new { groupId });
    }
}