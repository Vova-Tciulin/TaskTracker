using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Aggregators.Models;
using TaskTracker.Aggregators.Services;
using TaskTracker.Aggregators.Services.Group;
using TaskTracker.Aggregators.Services.Identity;
using TaskTracker.Aggregators.Services.Task;

namespace TaskTracker.Aggregators.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GroupController:ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly ITaskService _taskService;
    private readonly IIdentityService _identityService;
    private readonly ILogger<GroupController> _logger;
    private readonly IMapper _map;

    public GroupController(
        IGroupService groupService, ITaskService taskService, ILogger<GroupController> logger,
        IMapper map,IIdentityService identityService)
    {
        _groupService = groupService;
        _taskService = taskService;
        _logger = logger;
        _map = map;
        _identityService = identityService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetGroupById(Guid groupId)
    {
        var groupResponse = await _groupService.GetGroupById(groupId);
        var groupModel = new GroupModel()
        {
            Id = groupResponse.Id,
            AuthorId = groupResponse.AuthorId,
            Description = groupResponse.Description,
        };
        
        if (groupResponse.Tasks.Any())
        {
            var tasksResponse = await _taskService.GetTasksByGroupId(groupId);
            groupModel.Tasks = _map.Map<List<TaskModel>>(tasksResponse);
        }
        
        foreach (var user in groupResponse.Users)
        {
            var userResponse = await _identityService.GetUserInfoById(user.UserId);
            groupModel.Users.Add(userResponse);
        }

        foreach (var task in groupModel.Tasks)
        {
            var user = groupModel.Users.FirstOrDefault(u => u.Id == task.AuthorId.ToString()) ??
                       await _identityService.GetUserInfoById(task.AuthorId);

            task.User = user;
        }
        
        return Ok(groupModel);
    }
}