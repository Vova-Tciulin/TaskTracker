using Tasks.Query.Domain.Models;

namespace Tasks.Query.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<TaskEntity?> GetByIdAsync(Guid taskId);
    Task<List<TaskEntity>?> GetTasksByGroupIdAsync(Guid groupId);
    void RemoveTask(TaskEntity task);
    void UpdateTask(TaskEntity task);
    void CreateTask(TaskEntity task);
    Task SaveChangesAsync();
}