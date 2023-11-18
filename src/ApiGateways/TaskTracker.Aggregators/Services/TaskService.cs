using TaskTracker.Aggregators.HttpExtensions;
using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services;

public class TaskService:ITaskService
{
    private readonly HttpClient _client;

    public TaskService(HttpClient client)
    {
        _client = client;
    }

    
    public async Task<List<TaskResponse>> GetTasksByGroupId(Guid groupId)
    {
        var response = await _client.GetAsync($"/api/task/GetTasksByGroupId?groupId={groupId}");
        return await response.ReadContentAs<List<TaskResponse>>();
    }
}