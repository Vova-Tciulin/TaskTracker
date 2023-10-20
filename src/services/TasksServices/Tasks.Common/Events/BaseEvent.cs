﻿namespace Tasks.Common.Events;

public abstract class BaseEvent
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public int Version { get; set; }

    public BaseEvent(string type)
    {
        Type = type;
    }

    
}