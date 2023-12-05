using WebApp.Services.ModelDto;
using WebApp.Services.ModelDto.Task;

namespace WebApp.Services;

public interface ITaskService
{
    Task<TaskDto> CreateTask(CreateTaskDto model);
    Task UpdateTask(UpdateTaskDto model);
    Task RemoveTask(string taskId);
    Task ChangeTaskState(ChangeTaskStateDto model,string currentTaskState,string newTaskState);
    
    Task<TaskDto> GetTaskById(Guid taskId);
}