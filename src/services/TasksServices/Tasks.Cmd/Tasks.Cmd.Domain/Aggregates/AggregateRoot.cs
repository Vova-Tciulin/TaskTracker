﻿using System.Reflection;
using Tasks.Common.Events;

namespace Tasks.Cmd.Domain.Aggregates;

public abstract class AggregateRoot
{
    protected Guid _id;
    private readonly List<BaseEvent> _changes = new();
    
    public Guid Id
    {
        get { return _id; }
    }

    public int Version { get; set; } = -1;

    public IEnumerable<BaseEvent> GetUncommittedChanges()
    {
        return _changes;
    }
    
    public void MarkUncommittedChanges()
    {
        _changes.Clear();
    }

    private void ApplyChanges(BaseEvent @event, bool isNew)
    {
        var method = this.GetType().GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { @event.GetType() });

        if (method==null)
        {
            throw new NullReferenceException(
                $"Метод Apply в  {this.GetType()} с параметром: {@event.Type} не найден!");
        }

        method.Invoke(this, new object?[]{@event});

        if (isNew)
        {
            _changes.Add(@event);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChanges(@event,true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChanges(@event,false);
        }
    }
}