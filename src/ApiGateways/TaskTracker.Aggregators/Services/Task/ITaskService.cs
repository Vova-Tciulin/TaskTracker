using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services.Task;

public interface ITaskService
{
    Task<List<TaskResponse>> GetTasksByGroupId(Guid groupId);
}