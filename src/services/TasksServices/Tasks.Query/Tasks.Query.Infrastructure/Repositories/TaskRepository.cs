using Microsoft.EntityFrameworkCore;
using Tasks.Query.Domain.Models;
using Tasks.Query.Infrastructure.Data;

namespace Tasks.Query.Infrastructure.Repositories;

public class TaskRepository:ITaskRepository
{
    private readonly TaskDbContext _db;

    public TaskRepository(TaskDbContext db)
    {
        _db = db;
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid taskId)
    {
        return await _db.Tasks.FirstOrDefaultAsync(u => u.TaskId == taskId);
    }

    public async Task<List<TaskEntity>?> GetTasksByGroupIdAsync(Guid groupId)
    {
        return await _db.Tasks.Where(u => u.GroupId == groupId).ToListAsync();
    }

    public void RemoveTask(TaskEntity task)
    {
        _db.Tasks.Remove(task);
    }

    public void UpdateTask(TaskEntity task)
    {
        _db.Tasks.Update(task);
    }

    public void CreateTask(TaskEntity task)
    {
        _db.Tasks.Add(task);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}