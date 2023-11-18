using WebApp.Services.HttpExtensions;
using WebApp.Services.ModelDto.Group;

namespace WebApp.Services;

public class GroupService: IGroupService
{
    private readonly HttpClient _client;
    private readonly ILogger<GroupService> _logger;
    private readonly string _baseUrl;

    public GroupService(HttpClient client, ILogger<GroupService> logger,IConfiguration configuration)
    {
        _client = client;
        _logger = logger;
        _baseUrl = configuration["ApiGatewayUrl"];
    }

    public async Task CreateGroup(CreateGroupDto model)
    {
        string uri = ApiUrls.CreateGroupUrl(_baseUrl);
        
        _logger.LogInformation($"[CreateGroup] -> Calling {uri} to create the group");
        var response = await _client.PostAsJsonAsync(uri, model);
        _logger.LogInformation($"[CreateGroup] -> response code {response.StatusCode}");

        response.EnsureSuccessStatusCode();
    }

    public async Task AddUserToGroup(AddUserToGroupDto model)
    {
        var uri = ApiUrls.AddUserToGroupUrl(_baseUrl);
        
        _logger.LogInformation($"[AddUserToGroup] -> Calling {uri} to add user to group");
        var response = await _client.PutAsJsonAsync(uri, model);
        _logger.LogInformation($"[AddUserToGroup] -> response code {response.StatusCode}");

        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveGroup(Guid groupId)
    {
        var uri = ApiUrls.RemoveGroupUrl(_baseUrl, groupId);
        
        _logger.LogInformation($"[RemoveGroup] -> Calling {uri} to remove the group");
        var response = await _client.DeleteAsync(uri);
        _logger.LogInformation($"[RemoveGroup] -> response code {response.StatusCode}");

        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveUserFromGroup(RemoveUserFromGroupDto model)
    {
        var uri = ApiUrls.RemoveUserFromGroupUrl(_baseUrl);
        
        _logger.LogInformation($"[RemoveUserFromGroup] -> Calling {uri} to remove user from group");
        var response = await _client.PutAsJsonAsync(uri, model);
        _logger.LogInformation($"[RemoveUserFromGroup] -> response code {response.StatusCode}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<GroupAggregatorDto> GetGroupAggregatorById(string groupId)
    {
        var uri = ApiUrls.GetGroupAggregatorByIdUrl(_baseUrl,groupId);
        
        _logger.LogInformation($"[GetGroupAggregatorById] -> Calling {uri} to get group aggregator");
        var response = await _client.GetAsync(uri);
        _logger.LogInformation($"[GetGroupAggregatorById] -> response code {response.StatusCode}");

        return await response.ReadContentAs<GroupAggregatorDto>();
    }

    public async Task<List<GroupDto>> GetGroupsByUserId(string userId)
    {
        var uri = ApiUrls.GetGroupsByUserIdUrl(_baseUrl,userId);
        
        _logger.LogInformation($"[GetGroups] -> Calling {uri} to get groups");
        var response = await _client.GetAsync(uri);
        _logger.LogInformation($"[GetGroups] -> response code {response.StatusCode}");

        return await response.ReadContentAs<List<GroupDto>>();
    }
}