using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using Tasks.Cmd.Api.Models;
using Tasks.Cmd.Application.Features.Commands.CompleteTask;
using Tasks.Cmd.Application.Features.Commands.CreateTask;
using Tasks.Cmd.Application.Features.Commands.ExecuteTask;
using Tasks.Cmd.Application.Features.Commands.RemoveTask;
using Tasks.Cmd.Application.Features.Commands.UpdateTask;

namespace Tasks.Cmd.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController:ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _map;

    public TaskController(ILogger<TaskController> logger, IMediator mediator, IMapper map)
    {
        _logger = logger;
        _mediator = mediator;
        _map = map;
    }
    
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody]CreateTaskDto model)
    {
        _logger.LogInformation($"Create new Task with model: {JsonSerializer.Serialize(model)}");
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");

        if (userId==null)
        {
            throw new Exception("userId not exist!");
        }
        
        var command = _map.Map<CreateTaskCommand>(model);
        command.AuthorId=Guid.Parse(userId.Value);
        
        var task=await _mediator.Send(command);
        
        var taskDto = _map.Map<TaskDto>(model);
        taskDto.TaskId = task.Id;
        taskDto.State = 0;
        taskDto.AuthorId= Guid.Parse(userId.Value);
        taskDto.TaskCreated = task.TaskCreated;
        
        _logger.LogInformation($"Task created with Id: {task.Id}");
        return Ok(taskDto);
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> UpdateTask([FromBody]UpdateTaskDto model)
    {
        _logger.LogInformation($"Update Task with model: {JsonSerializer.Serialize(model)}");
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");

        if (userId==null)
        {
            _logger.LogInformation($"UserId in Claims not exist!");
            throw new Exception("userId not exist!");
        }
        
        var command = _map.Map<UpdateTaskCommand>(model);
        command.AuthorId=Guid.Parse(userId.Value);
        
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("[action]")]
    public async Task<IActionResult> RemoveTask(Guid taskId)
    {
        _logger.LogInformation($"Remove Task with id: {taskId}");
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");
        if (userId==null)
        {
            _logger.LogInformation($"UserId in Claims not exist!");
            throw new Exception("userId not exist!");
        }

        var command = new RemoveTaskCommand()
        {
            AuthorId = Guid.Parse(userId.Value),
            TaskId = taskId
        };
        
        var res = await _mediator.Send(command);

        _logger.LogInformation($"Execution result: {res}");
        
        if (!res)
        {
            return NoContent();
        }
        
        return Ok();
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> ExecuteTask([FromBody] ExecuteTaskDto model)
    {
        _logger.LogInformation($"Execute Task with model: {JsonSerializer.Serialize(model)}");
        
        var command = _map.Map<ExecuteTaskCommand>(model);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> CompleteTask([FromBody] CompleteTaskDto model)
    {
        _logger.LogInformation($"Complete Task with model: {JsonSerializer.Serialize(model)}");
        
        var command = _map.Map<CompleteTaskCommand>(model);
        await _mediator.Send(command);

        return Ok();
    }
    
    
}