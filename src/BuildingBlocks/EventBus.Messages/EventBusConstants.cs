namespace EventBus.Messages;

public static class EventBusConstants
{
    //Queues
    public const string TaskQueryQueue = "taskQuery-queue";
    
    //Topic routing keu
    public const string TaskAllEvents = "tasks.*";
    public const string TaskCreatedEvents = "tasks.TaskCreatedEvent";
}