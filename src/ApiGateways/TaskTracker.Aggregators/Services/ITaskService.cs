using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services;

public interface ITaskService
{
    Task<List<TaskResponse>> GetTasksByGroupId(Guid groupId);
}