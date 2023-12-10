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
        _logger.LogInformation($"Create new Group");
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");
        if (userId==null)
        {
            _logger.LogInformation($"UserId in Claims not exist!");
            throw new Exception("userId not exist!");
        }

        var group = await _mediator.Send(new CreateGroupCommand()
        {
            UserId = Guid.Parse(userId.Value),
            Description = model.Description
        });
        
        
        _logger.LogInformation($"Group created with Id: {group.Id}");
        return Ok(new GroupDto()
        {
            Id = group.Id,
            AuthorId = Guid.Parse(userId.Value),
            Description = model.Description
        });
    }
    
    [Route("[action]")]
    [HttpDelete]
    public async Task<IActionResult> RemoveGroup(Guid groupId)
    {
        _logger.LogInformation($"Remove group: {groupId}");
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sub");
        if (userId==null)
        {
            _logger.LogInformation($"UserId in Claims not exist!");
            throw new Exception("userId not exist!");
        }

        var command = new RemoveGroupCommand()
        {
            UserId = Guid.Parse(userId.Value),
            GroupId = groupId
        };
        
        var res = await _mediator.Send(command);
        
        _logger.LogInformation($"result of removing a group: {res}");
        return Ok(res);
    }

    
    [Route("[action]")]
    [HttpPut]
    public async Task<IActionResult> AddUserToGroup([FromBody] AddUserToGroup model)
    {
        _logger.LogInformation($"Add user: {model.NickNameOrEmail} to group: {model.GroupId}");

        var command = _map.Map<AddUserCommand>(model);
        var res = await _mediator.Send(command);
        
        _logger.LogInformation($"result of adding a user: {res}");
        return Ok(res);
    }
    
    [Route("[action]")]
    [HttpPut]
    public async Task<IActionResult> RemoveUserFromGroup([FromBody] RemoveUserFromGroup model)
    {
        _logger.LogInformation($"Remove user: {model.UserId} from group: {model.GroupId}");

        var command = _map.Map<RemoveUserCommand>(model);
        var res = await _mediator.Send(command);
        
        _logger.LogInformation($"result of removing a user: {res}");
        return Ok(res);
    }
}