using TaskTracker.Aggregators.HttpExtensions;
using TaskTracker.Aggregators.Models;

namespace TaskTracker.Aggregators.Services.Identity;

public class IdentityService:IIdentityService
{
    private readonly HttpClient _client;

    public IdentityService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserDto> GetUserInfoById(Guid userId)
    {
        var response = await _client.GetAsync($"/api/user/getUserInfoById?userId={userId}");
        return await response.ReadContentAs<UserDto>();
    }
}