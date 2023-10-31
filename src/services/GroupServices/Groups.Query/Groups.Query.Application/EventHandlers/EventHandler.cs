using AutoMapper;
using Group.Common.Events;
using Groups.Query.Domain.Entities;
using Groups.Query.Domain.Exceptions;
using Groups.Query.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Groups.Query.Application.EventHandlers;

public class EventHandler: IEventHandler
{
    private readonly IGroupRepository _groupRepository;
    private readonly ILogger<EventHandler> _logger;
    private readonly IMapper _map;

    public EventHandler(IGroupRepository groupRepository, ILogger<EventHandler> logger, IMapper map)
    {
        _groupRepository = groupRepository;
        _logger = logger;
        _map = map;
    }

    public async Task On(GroupCreatedEvent @event)
    {
        var group = _map.Map<GroupEntity>(@event);
        group.Users.Add(new GroupUser(){UserId = @event.AuthorId, GroupId = @event.Id});
        _groupRepository.CreateGroup(group);
        await _groupRepository.SaveChangesAsync();
    }

    public async Task On(GroupRemovedEvent @event)
    {
        var group = await _groupRepository.GetByIdAsync(@event.Id);
        if (group==null)
        {
            _logger.LogWarning($"group with Id: {@event.Id} was not found!");
            throw new NotFoundException($"group with Id: {@event.Id} was not found!");
        }
        
        _groupRepository.RemoveGroup(group);
        await _groupRepository.SaveChangesAsync();
    }

    public async Task On(GroupTaskAddedEvent @event)
    {
        var group = await _groupRepository.GetByIdAsync(@event.Id);
        if (group==null)
        {
            _logger.LogWarning($"group with Id: {@event.Id} was not found!");
            throw new NotFoundException($"group with Id: {@event.Id} was not found!");
        }
        
        group.Tasks.Add(new GroupTask(){GroupId = @event.Id, TaskId = @event.TaskId});
        _groupRepository.UpdateGroup(group);
        await _groupRepository.SaveChangesAsync();
    }

    public async Task On(GroupTaskRemovedEvent @event)
    {
        var group = await _groupRepository.GetByIdAsync(@event.Id);
        if (group==null)
        {
            _logger.LogWarning($"group with Id: {@event.Id} was not found!");
            throw new NotFoundException($"group with Id: {@event.Id} was not found!");
        }

        var task = group.Tasks.FirstOrDefault(u => u.TaskId == @event.TaskId);
        if (task==null)
        {
            _logger.LogWarning($"task with Id: {@event.TaskId} was not found!");
            throw new NotFoundException($"task with Id: {@event.TaskId} was not found!");
        }

        group.Tasks.Remove(task);
        _groupRepository.UpdateGroup(group);
        await _groupRepository.SaveChangesAsync();
    }

    public async Task On(GroupUserAddedEvent @event)
    {
        var group = await _groupRepository.GetByIdAsync(@event.Id);
        if (group==null)
        {
            _logger.LogWarning($"group with Id: {@event.Id} was not found!");
            throw new NotFoundException($"group with Id: {@event.Id} was not found!");
        }

        group.Users.Add(new GroupUser(){GroupId = @event.Id, UserId = @event.UserId});
        _groupRepository.UpdateGroup(group);
        await _groupRepository.SaveChangesAsync();
    }

    public async Task On(GroupUserRemovedEvent @event)
    {
        var group = await _groupRepository.GetByIdAsync(@event.Id);
        if (group==null)
        {
            _logger.LogWarning($"group with Id: {@event.Id} was not found!");
            throw new NotFoundException($"group with Id: {@event.Id} was not found!");
        }

        var user = group.Users.FirstOrDefault(u => u.UserId == @event.UserId);
        if (user==null)
        {
            _logger.LogWarning($"user with Id: {@event.UserId} was not found!");
            throw new NotFoundException($"user with Id: {@event.UserId} was not found!");
        }

        group.Users.Remove(user);
        _groupRepository.UpdateGroup(group);
        await _groupRepository.SaveChangesAsync();
    }
}