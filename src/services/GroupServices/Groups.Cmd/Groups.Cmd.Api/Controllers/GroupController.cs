using AutoMapper;
using Groups.Cmd.Api.Models;
using Groups.Cmd.Application.Features.Commands.AddUser;
using Groups.Cmd.Application.Features.Commands.CreateGroup;
using Groups.Cmd.Application.Features.Commands.RemoveGroup;
using Groups.Cmd.Application.Features.Commands.RemoveUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Groups.Cmd.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GroupController: ControllerBase
{
    private readonly ILogger<GroupController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _map;

    public GroupController(ILogger<GroupController> logger, IMediator mediator, IMapper map)
    {
        _logger = logger;
        _mediator = mediator;
        _map = map;
    }

    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroup model)
    {
        _logger.LogInformation($"Create new Group with userId: {model.UserId}");

        var groupId = await _mediator.Send(new CreateGroupCommand() { UserId = model.UserId });
        
        _logger.LogInformation($"Group created with Id: {groupId}");
        return Ok(groupId);
    }
    
    [Route("[action]")]
    [HttpDelete]
    public async Task<IActionResult> RemoveGroup([FromBody] RemoveGroup model)
    {
        _logger.LogInformation($"Remove group: {model.GroupId}");

        var command = _map.Map<RemoveGroupCommand>(model);
        var res = await _mediator.Send(command);
        
        _logger.LogInformation($"result of removing a group: {res}");
        return Ok(res);
    }

    
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> AddUserToGroup([FromBody] AddUserToGroup model)
    {
        _logger.LogInformation($"Add user: {model.UserId} to group: {model.GroupId}");

        var command = _map.Map<AddUserCommand>(model);
        var res = await _mediator.Send(command);
        
        _logger.LogInformation($"result of adding a user: {res}");
        return Ok(res);
    }
    
    [Route("[action]")]
    [HttpDelete]
    public async Task<IActionResult> RemoveUserFromGroup([FromBody] RemoveUserFromGroup model)
    {
        _logger.LogInformation($"Remove user: {model.UserId} from group: {model.GroupId}");

        var command = _map.Map<RemoveUserCommand>(model);
        var res = await _mediator.Send(command);
        
        _logger.LogInformation($"result of removing a user: {res}");
        return Ok(res);
    }
}