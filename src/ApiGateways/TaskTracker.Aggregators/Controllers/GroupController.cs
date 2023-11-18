using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Aggregators.Models;
using TaskTracker.Aggregators.Services;

namespace TaskTracker.Aggregators.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GroupController:ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly ITaskService _taskService;
    private readonly ILogger<GroupController> _logger;
    private readonly IMapper _map;

    public GroupController(IGroupService groupService, ITaskService taskService, ILogger<GroupController> logger, IMapper map)
    {
        _groupService = groupService;
        _taskService = taskService;
        _logger = logger;
        _map = map;
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
            Users = groupResponse.Users
        };
        
        if (groupResponse.Tasks.Any())
        {
            var tasksResponse = await _taskService.GetTasksByGroupId(groupId);
            groupModel.Tasks = _map.Map<List<TaskModel>>(tasksResponse);
        }
        
        return Ok(groupModel);
    }
}