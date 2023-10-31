using Groups.Query.Application.Queries.GetGroup;
using Groups.Query.Application.Queries.GetGroupsCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Groups.Query.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController:ControllerBase
{
    private readonly ILogger<GroupController> _logger;
    private readonly IMediator _mediator;

    public GroupController(ILogger<GroupController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetGroupById(Guid id)
    {
        var group = await _mediator.Send(new GetGroupQuery() { GroupId = id });
        return Ok(group);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetGroupCollectionByUserId(Guid userId)
    {
        var group = await _mediator.Send(new GetGroupsCollectionQuery(){UserId = userId});
        return Ok(group);
    }
}