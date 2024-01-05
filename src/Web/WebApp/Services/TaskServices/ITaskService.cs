using WebApp.Services.ModelDto.Task;

namespace WebApp.Services.TaskServices;

public interface ITaskService
{
    Task<TaskDto> CreateTask(CreateTaskDto model);
    Task UpdateTask(UpdateTaskDto model);
    Task RemoveTask(string taskId);
    Task ChangeTaskState(ChangeTaskStateDto model,string currentTaskState,string newTaskState);
    
    Task<TaskDto> GetTaskById(Guid taskId);
}