using WebApp.Services.ModelDto.Task;

namespace WebApp.Services.Moq;

public class TaskServiceMoq:ITaskService
{
    private readonly Db _db;

    public TaskServiceMoq(Db db)
    {
        _db = db;
    }

    public Task<TaskDto> CreateTask(CreateTaskDto model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTask(UpdateTaskDto model)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTask(string taskId)
    {
        throw new NotImplementedException();
    }

    public Task ChangeTaskState(ChangeTaskStateDto model, string currentTaskState, string newTaskState)
    {
        throw new NotImplementedException();
    }


    public async Task<TaskDto> GetTaskById(Guid taskId)
    {
        return await Task.FromResult(_db.Tasks[0]);
    }
}