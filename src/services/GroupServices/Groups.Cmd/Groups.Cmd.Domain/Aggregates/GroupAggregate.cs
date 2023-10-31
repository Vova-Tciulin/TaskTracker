using Group.Common.Events;

namespace Groups.Cmd.Domain.Aggregates;

public class GroupAggregate:AggregateRoot
{
    private Guid _authorId;
    private List<Guid> _users=new();
    private List<(Guid taskId, Guid authorId)> _tasks = new();
    private bool _isActive;

    public GroupAggregate()
    {
        
    }
    public GroupAggregate(Guid groupId, Guid userId)
    {
        RaiseEvent(new GroupCreatedEvent(){AuthorId = userId, Id = groupId});
    }

    private void Apply(@GroupCreatedEvent @event)
    {
        _id = @event.Id;
        _authorId = @event.AuthorId;
        _users.Add(_authorId);
        _isActive = true;
    }

    public void AddUser(Guid authorId, Guid userId)
    {
        if (_authorId!=authorId)
        {
            throw new InvalidOperationException("Данный пользователь не имеет прав для добавления пользователей!");
        }
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная группа удалена!");
        }

        if (_users.Contains(userId))
        {
            throw new InvalidOperationException("Данный пользователь уже состоит в группе!");
        }
        
        RaiseEvent(new GroupUserAddedEvent(){Id = _id, UserId = userId});
    }

    private void Apply(GroupUserAddedEvent @event)
    {
        _users.Add(@event.UserId);
    }

    public void AddTask(Guid userId, Guid taskId)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная группа удалена!");
        }

        if (!_users.Contains(userId))
        {
            throw new InvalidOperationException("Данный пользователь не может создать задачу для этой группы!");
        }
        
        RaiseEvent(new GroupTaskAddedEvent(){Id = _id, TaskId = taskId, UserId = userId});
    }

    private void Apply(GroupTaskAddedEvent @event)
    {
        _tasks.Add((@event.TaskId, @event.UserId));
    }

    public void RemoveUser(Guid authorId, Guid userId)
    {
        if (_authorId!=authorId)
        {
            throw new InvalidOperationException("Данный пользователь не имеет прав для добавления пользователей!");
        }
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная группа удалена!");
        }

        if (!_users.Contains(userId))
        {
            throw new InvalidOperationException("Данный пользователь не состоит в группе!");
        }

        if (userId==_authorId)
        {
            throw new InvalidOperationException("Нельзя удалить автора группы!");
        }
        RaiseEvent(new GroupUserRemovedEvent(){ Id = _id, UserId = userId});
    }

    private void Apply(GroupUserRemovedEvent @event)
    {
        _users.Remove(@event.UserId);
    }

    public void RemoveTask(Guid taskId)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная группа удалена!");
        }

        if (!_tasks.Exists(u=>u.taskId==taskId))
        {
            throw new InvalidOperationException("Данная задача не существует!");
        }
        
        RaiseEvent(new GroupTaskRemovedEvent(){Id = _id, TaskId = taskId});
    }

    private void Apply(GroupTaskRemovedEvent @event)
    {
        var task = _tasks.FirstOrDefault(u => u.taskId == @event.TaskId);
        _tasks.Remove(task);
    }

    public void RemoveGroup(Guid authorId)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Данная группа удалена!");
        }
        if (authorId!=_authorId)
        {
            throw new InvalidOperationException("Данный пользователь не имеет прав для удаления группы!");
        }
        RaiseEvent(new GroupRemovedEvent(){Id = _id, RemovedTasks = _tasks});
    }

    private void Apply(GroupRemovedEvent @event)
    {
        _isActive = false;
    }
}