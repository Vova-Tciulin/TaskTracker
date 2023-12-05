using Tasks.Cmd.Domain.Models;
using Tasks.Common;
using Tasks.Common.enums;
using Tasks.Common.Events;

namespace Tasks.Cmd.Domain.Aggregates;

public class TaskAggregate:AggregateRoot
{
    private Guid _groupId;
    private Guid _authorId;
    private TaskState _state;
    private bool _isActive;
    private Guid? _workerId;
    private DateTime _taskCreated;
    
    public Guid GroupId => _groupId;
    public DateTime TaskCreated => _taskCreated;
    public TaskState TaskState => _state;
    public Guid? WorkerId => _workerId;

    public TaskAggregate()
    {
        
    }
    public TaskAggregate(Guid id,Guid groupId, Guid authorId,string title, string task,DateTime deadline)
    {
        RaiseEvent(new TaskCreatedEvent()
        {
            Id=id,
            GroupId=groupId,
            AuthorId = authorId,
            Title = title,
            Task = task,
            State = TaskState.New,
            TaskCreated = DateTime.Now,
            DeadLine = deadline
        });
    }

    private void Apply(TaskCreatedEvent @event)
    {
        _id = @event.Id;
        _groupId = @event.GroupId;
        _authorId = @event.AuthorId;
        _state = @event.State;
        _isActive = true;
        _taskCreated = @event.TaskCreated;
    }

    public void UpdateTask(Guid authorId, string newTask)
    {
        if (_authorId!=authorId)
        {
            throw new InvalidOperationException("Данный пользователь не является автором этой задачи!");
        }

        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача удалена!");
        }
        
        RaiseEvent(new TaskUpdatedTaskEvent(){Id = _id, Task = newTask});
    }

    private void Apply(TaskUpdatedTaskEvent @event)
    {
        
    }

    public void UpdateDeadline(Guid authorId, DateTime newDeadline)
    {
        if (_authorId!=authorId)
        {
            throw new InvalidOperationException("Данный пользователь не является автором этой задачи!");
        }

        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача удалена!");
        }
        
        RaiseEvent(new TaskUpdatedDeadlineEvent(){Id = _id, NewDeadline = newDeadline});
    }

    private void Apply(TaskUpdatedDeadlineEvent @event)
    {
        
    }

    public void RemoveTask(Guid authorId)
    {
        if (_authorId!=authorId)
        {
            throw new InvalidOperationException("Данный пользователь не является автором этой задачи!");
        }
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача уже удалена!");
        }
        
        RaiseEvent(new TaskRemovedEvent(){Id = _id, AuthorId = authorId, GroupId = _groupId});
    }

    private void Apply(TaskRemovedEvent @event)
    {
        _isActive = false;
    }

    public void TakeTaskInWork(Guid workerId)
    {
        if (_state!=TaskState.New)
        {
            throw new InvalidOperationException("Данная задача не является новой!");
        }
        
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача уже удалена!");
        }
        
        RaiseEvent(new TaskTakenOnWorkEvent(){Id=_id, DateTime = DateTime.Now, WorkerId = workerId});
    }

    private void Apply(TaskTakenOnWorkEvent @event)
    {
        _workerId = @event.WorkerId;
        _state = TaskState.InWork;
    }

    public void CompleteTask(Guid workerId)
    {
        if (_workerId!=workerId)
        {
            throw new InvalidOperationException("Данный пользователь не выполняет эту задачу!");
        }
        
        if (_state!=TaskState.InWork)
        {
            throw new InvalidOperationException("Данная задача не может быть завершена!");
        }
        
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача уже удалена!");
        }
        
        RaiseEvent(new TaskCompletedEvent(){Id = _id, WorkerId = workerId, CompletedDateTime = DateTime.Now});
    }

    private void Apply(TaskCompletedEvent @event)
    {
        _state = TaskState.Finished;
    }

    public void UpdateTitle(Guid authorId, string title)
    {
        if (_authorId!=authorId)
        {
            throw new InvalidOperationException("Данный пользователь не является автором этой задачи!");
        }

        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача удалена!");
        }
        
        RaiseEvent(new TaskUpdatedTitleEvent()
        {
            Id = _id,
            Title = title
        });
    }

    private void Apply(TaskUpdatedTitleEvent @event)
    {
        
    }

    public void ReturnTaskToNewState(Guid workerId)
    {
        if (_workerId!=workerId)
        {
            throw new InvalidOperationException("Данный пользователь не работает над этой задачей!");
        }

        if (!_isActive)
        {
            throw new InvalidOperationException("Данная задача удалена!");
        }
        
        RaiseEvent(new TaskReturnToNewState()
        {
            Id = _id,
            WorkerId = workerId
        });

    }

    private void Apply(TaskReturnToNewState @event)
    {
        _workerId = null;
        _state = TaskState.New;
    }
    
}