using TaskTracker.Aggregators.HttpExtensions;
using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services;

public class GroupService:IGroupService
{
    private readonly HttpClient _client;

    public GroupService(HttpClient client)
    {
        _client = client;
    }

    public async Task<GroupResponse> GetGroupById(Guid groupId)
    {
        var response = await _client.GetAsync($"/api/group/getGroupById?id={groupId}");
        return await response.ReadContentAs<GroupResponse>();
    }
}