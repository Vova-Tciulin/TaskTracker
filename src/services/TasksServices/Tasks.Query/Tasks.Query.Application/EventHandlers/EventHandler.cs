using AutoMapper;
using Microsoft.Extensions.Logging;
using Tasks.Common.enums;
using Tasks.Common.Events;
using Tasks.Query.Domain.Exceptions;
using Tasks.Query.Domain.Models;
using Tasks.Query.Infrastructure.Repositories;

namespace Tasks.Query.Application.EventHandlers;

public class EventHandler:IEventHandlers
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<EventHandler> _logger;
    private readonly IMapper _map;

    public EventHandler(ITaskRepository taskRepository, ILogger<EventHandler> logger, IMapper map)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _map = map;
    }

    public async Task On(TaskCreatedEvent @event)
    {
        var task = _map.Map<TaskEntity>(@event);
        _taskRepository.CreateTask(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task On(TaskCompletedEvent @event)
    {
        var task = await _taskRepository.GetByIdAsync(@event.Id);
        if (task==null)
        {
            _logger.LogError($"Task with id: {@event.Id} was not found");
            throw new NotFoundException($"Task with id: {@event.Id} was not found");
        }
        task.State = TaskState.Finished;
        task.CompletedDateTime = @event.CompletedDateTime;
        
        _taskRepository.UpdateTask(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task On(TaskRemovedEvent @event)
    {
        var task = await _taskRepository.GetByIdAsync(@event.Id);
        if (task==null)
        {
            _logger.LogError($"Task with id: {@event.Id} was not found");
            throw new NotFoundException($"Task with id: {@event.Id} was not found");
        }
        
        _taskRepository.RemoveTask(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task On(TaskTakenOnWorkEvent @event)
    {
        var task = await _taskRepository.GetByIdAsync(@event.Id);
        if (task==null)
        {
            _logger.LogError($"Task with id: {@event.Id} was not found");
            throw new NotFoundException($"Task with id: {@event.Id} was not found");
        }

        task.WorkerId = @event.WorkerId;
        task.State = TaskState.InWork;
        
        _taskRepository.UpdateTask(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task On(TaskUpdatedDeadlineEvent @event)
    {
        var task = await _taskRepository.GetByIdAsync(@event.Id);
        if (task==null)
        {
            _logger.LogError($"Task with id: {@event.Id} was not found");
            throw new NotFoundException($"Task with id: {@event.Id} was not found");
        }
        
        task.DeadLine = @event.NewDeadline;
        
        _taskRepository.UpdateTask(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task On(TaskUpdatedTaskEvent @event)
    {
        var task = await _taskRepository.GetByIdAsync(@event.Id);
        if (task==null)
        {
            _logger.LogError($"Task with id: {@event.Id} was not found");
            throw new NotFoundException($"Task with id: {@event.Id} was not found");
        }

        task.Task = @event.Task;
        
        _taskRepository.UpdateTask(task);
        await _taskRepository.SaveChangesAsync();
    }
}