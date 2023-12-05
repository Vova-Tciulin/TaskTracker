using WebApp.Services.HttpExtensions;
using WebApp.Services.ModelDto.Task;

namespace WebApp.Services;

public class TaskService:ITaskService
{
    private readonly HttpClient _client;
    private readonly ILogger<GroupService> _logger;
    private readonly string _baseUrl;

    public TaskService(HttpClient client, ILogger<GroupService> logger,IConfiguration configuration)
    {
        _client = client;
        _logger = logger;
        _baseUrl = configuration["ApiGatewayUrl"];
    }

    public async Task<TaskDto> CreateTask(CreateTaskDto model)
    {
        var uri = ApiUrls.CreateTaskUrl(_baseUrl);
        
        _logger.LogInformation($"[CreateTask] -> Calling {uri} to create the task");
        var response = await _client.PostAsJsonAsync(uri, model);
        _logger.LogInformation($"[CreateTask] -> response code {response.StatusCode}");

        return await response.ReadContentAs<TaskDto>();
    }

    public async Task UpdateTask(UpdateTaskDto model)
    {
        var uri=ApiUrls.UpdateTaskUrl(_baseUrl);
        
        _logger.LogInformation($"[UpdateTask] -> Calling {uri} to update the task");
        var response = await _client.PutAsJsonAsync(uri, model);
        _logger.LogInformation($"[UpdateTask] -> response code {response.StatusCode}");

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Something went wrong calling the API: {msg}");
        }
    }

    public async Task RemoveTask(string taskId)
    {
        var uri=ApiUrls.RemoveTaskUrl(_baseUrl,taskId);
        
        _logger.LogInformation($"[RemoveTask] -> Calling {uri} to remove the task");
        var response = await _client.DeleteAsync(uri);
        _logger.LogInformation($"[RemoveTask] -> response code {response.StatusCode}");

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Something went wrong calling the API: {msg}");
        }
    }

    public async Task ChangeTaskState(ChangeTaskStateDto model,string currentTaskState, string newTaskState)
    {

        var uri = currentTaskState switch
        {
            "New" => ApiUrls.ExecuteTaskUrl(_baseUrl),
            "InWork" => newTaskState == "New"
                ? ApiUrls.ReturnTaskToNewState(_baseUrl)
                : ApiUrls.CompleteTaskUrl(_baseUrl),
            _ => throw new ArgumentOutOfRangeException($"данного текущего состояния не существует!")
        };
        
        _logger.LogInformation($"[ChangeTaskState] -> Calling {uri} to change task state");
        var response = await _client.PutAsJsonAsync(uri, model);
        _logger.LogInformation($"[ChangeTaskState] -> response code {response.StatusCode}");

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Something went wrong calling the API: {msg}");
        }
    }

    public async Task<TaskDto> GetTaskById(Guid taskId)
    {
        var uri=ApiUrls.GetTaskUrl(_baseUrl,taskId);
        
        _logger.LogInformation($"[RemoveTask] -> Calling {uri} to remove the task");
        var response = await _client.GetAsync(uri);
        _logger.LogInformation($"[RemoveTask] -> response code {response.StatusCode}");

        return await response.ReadContentAs<TaskDto>();
    }
}