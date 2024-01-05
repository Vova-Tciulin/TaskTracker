using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Task;
using WebApp.Services;
using WebApp.Services.Group;
using WebApp.Services.ModelDto.Task;

using WebApp.Services.TaskServices;

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
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskVm model)
    {
        _logger.LogInformation($"model: {JsonSerializer.Serialize(model)}");

        var newTaskDto= await _taskService.CreateTask(new CreateTaskDto()
        {
            GroupId = model.GroupId,
            Task = model.Task,
            Title = model.Title,
            DeadLine = DateTime.Parse(model.DeadLine)
        });

        var taskVm = _map.Map<TaskVm>(newTaskDto);
        return RedirectToAction("GetGroup", "Group", new { model.GroupId });
    }

   

    [HttpDelete]
    public async Task<IActionResult> RemoveTask(string taskId)
    {
        _logger.LogInformation($"invoke removeTask with id:{taskId}");
        await _taskService.RemoveTask(taskId);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> ChangeTaskState(Guid taskId,string currentState, string newState)
    {
       var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");

       _logger.LogInformation($"change taskState. CurrentState: {currentState}, newState: {newState}");
       
       var changeTaskStateDto = new ChangeTaskStateDto()
       {
           TaskId = taskId,
           WorkerId = Guid.Parse(userId.Value),
       };

       await _taskService.ChangeTaskState(changeTaskStateDto, currentState, newState);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTask(Guid taskId)
    {
        var taskDto = await _taskService.GetTaskById(taskId);

        var model = _map.Map<UpdateTaskVm>(taskDto);

        return PartialView("UpdateTaskPartial",model);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskVm model)
    {
        _logger.LogInformation($"[update task]. Model: {JsonSerializer.Serialize(model)}");
        
        var taskDto = await _taskService.GetTaskById(model.TaskId);

        var updateTaskDto = new UpdateTaskDto()
        {
            TaskId = model.TaskId,
            NewTask = model.Task==taskDto.Task? null:model.Task,
            NewTitle = model.Title==taskDto.Title? null:model.Title,
            NewDeadLine = DateTime.Parse(model.DeadLine)==taskDto.DeadLine? null:DateTime.Parse(model.DeadLine),
        };

        if (updateTaskDto.NewTask==null&& updateTaskDto.NewTitle==null&& updateTaskDto.NewDeadLine==null)
        {
            return RedirectToAction("GetGroup", "Group", new { model.GroupId });
        }

        await _taskService.UpdateTask(updateTaskDto);
        
        return RedirectToAction("GetGroup", "Group", new { model.GroupId });
    }
    
    
    
}