namespace EventBus.Messages;

public static class EventBusConstants
{
    //Queues
    public const string TaskQueryQueue = "taskQuery-queue";
    public const string TaskCmdQueue = "taskCmd-queue";
    
    public const string GroupQueryQueue = "groupQuery-queue";
    public const string GroupCmdQueue = "groupCmd-queue";
    
    //Topic routing key
    public const string TaskAllEvents = "tasks.*";
    public const string TaskCreatedEvents = "tasks.TaskCreatedEvent";
    public const string TaskRemovedEvents = "tasks.TaskRemovedEvent";
    
    public const string GroupAllEvents = "groups.*";
    public const string GroupRemovedEvents = "groups.GroupRemovedEvent";
    public const string GroupRemovedUserEvents = "groups.GroupUserRemovedEvent";
}