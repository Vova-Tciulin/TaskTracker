using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Task;
using WebApp.Services;
using WebApp.Services.ModelDto.Task;

namespace WebApp.Controllers;

[Authorize]
public class TaskController:Controller
{
    private readonly IMapper _map;
    private readonly ILogger<HomeController> _logger;
    private readonly IGroupService _groupService;
    private readonly ITaskService _taskService;

    public TaskController(IMapper map, ILogger<HomeController> logger, IGroupService groupService, ITaskService taskService)
    {
        _map = map;
        _logger = logger;
        _groupService = groupService;
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult CreateTask(Guid groupId)
    {
        _logger.LogInformation("Invoke CreateTask");
        var createTaskVm = new CreateTaskVm
        {
            GroupId = groupId,
        };

        return PartialView("CreateTaskPartial", createTaskVm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskVm model)
    {
        _logger.LogInformation($"model: {JsonSerializer.Serialize(model)}");

        await _taskService.CreateTask(new CreateTaskDto()
        {
            GroupId = model.GroupId,
            Task = model.Task,
            Title = model.Title,
            DeadLine = DateTime.Parse(model.DeadLine)
        });
        
        return RedirectToAction("Index","Home");
    }

   

    [HttpDelete]
    public async Task<IActionResult> RemoveTask(string taskId)
    {
        _logger.LogInformation($"invoke removeTask with id:{taskId}");
        await _taskService.RemoveTask(taskId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> ChangeTaskState(Guid taskId, string currentState)
    {
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");
        
        await _taskService.ChangeTaskState(new ChangeTaskStateDto()
        {
            TaskId = taskId,
            WorkerId = Guid.Parse(userId.Value),
        }, currentState);

        return Ok();
    }
}