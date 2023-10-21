using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Query.Application.Queries.GetTask;
using Tasks.Query.Application.Queries.GetTasksCollection;

namespace Task.Query.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController:ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaskController> _logger;

    public TaskController(IMediator mediator, ILogger<TaskController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        _logger.LogInformation($"TaskId: {taskId}");
        var query = new GetTaskQuery() { TaskId = taskId };
        var task = await _mediator.Send(query);
        _logger.LogInformation($"task: {JsonSerializer.Serialize(task)}");
        return Ok(task);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTasksByGroupId(Guid groupId)
    {
        var task = await _mediator.Send(new GetTasksCollectionQuery(){GroupId = groupId});
        return Ok(task);
    }
    
}