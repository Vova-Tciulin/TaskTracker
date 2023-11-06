using Tasks.Cmd.Application.Extensions;
using Tasks.Cmd.Application.Models;

namespace Tasks.Cmd.Application.Services;

public class GroupService:IGroupService
{
    private readonly HttpClient _httpClient;

    public GroupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GroupModel> GetGroupById(Guid groupId)
    {
        var response = await _httpClient.GetAsync($"/api/Group/GetGroupById?id={groupId}");
        return await response.ReadContentAs<GroupModel>();
    }
}