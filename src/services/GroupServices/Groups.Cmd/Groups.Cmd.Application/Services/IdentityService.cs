using Groups.Cmd.Application.Extensions;
using Groups.Cmd.Application.Models;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.Services;

public class IdentityService:IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<IdentityService> _logger;

    public IdentityService(HttpClient httpClient, ILogger<IdentityService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<UserDto> GetUserByNickNameOrEmail(string name)
    {
        _logger.LogInformation($"Invoke identityService with nickName or Email: {name}");

        var response= await _httpClient.GetAsync($"api/user/GetUserInfoByNickNameOrEmail?name={name}");
        return await response.ReadContentAs<UserDto>();
    }
}