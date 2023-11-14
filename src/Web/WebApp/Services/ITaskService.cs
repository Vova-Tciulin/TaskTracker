using WebApp.Services.ModelDto;
using WebApp.Services.ModelDto.Task;

namespace WebApp.Services;

public interface ITaskService
{
    Task CreateTask(CreateTaskDto model);
    Task UpdateTask(UpdateTaskDto model);
    Task RemoveTask(Guid taskId);
    Task ChangeTaskState(ChangeTaskStateDto model,string currentTaskState);
    
    Task<TaskDto> GetTaskById(Guid taskId);
}