using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Services;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application.Features.Commands.CreateTask;
using System.Threading.Tasks;

public class CreateTaskCommandHandler:IRequestHandler<CreateTaskCommand, TaskAggregate>
{
    private readonly IEventSourcingHandler<TaskAggregate> _eventSourcingHandler;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly IGroupService _groupService;

    public CreateTaskCommandHandler(IEventSourcingHandler<TaskAggregate> eventSourcingHandler, ILogger<CreateTaskCommandHandler> logger, IGroupService groupService)
    {
        _eventSourcingHandler = eventSourcingHandler;
        _logger = logger;
        _groupService = groupService;
    }

    public async Task<TaskAggregate> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling CreateTask with authorId: {request.AuthorId}");

        var group = await _groupService.GetGroupById(request.GroupId);
        if (!group.Users.Exists(u=>u.UserId==request.AuthorId))
        {
            _logger.LogWarning(
                $"user with id: {request.AuthorId} isn't a member of the group with id: {request.GroupId}");
            
            throw new InvalidOperationException(
                $"user with id: {request.AuthorId} isn't a member of the group with id: {request.GroupId}");
        }
        
        var aggregate = new TaskAggregate(Guid.NewGuid(),request.GroupId, request.AuthorId, request.Task,
            request.DeadLine);
        
        await _eventSourcingHandler.SaveAsync(aggregate);
        
        return aggregate;
    }
}