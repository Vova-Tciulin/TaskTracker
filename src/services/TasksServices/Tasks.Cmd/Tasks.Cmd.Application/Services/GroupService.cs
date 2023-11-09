using System.Net;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tasks.Cmd.Application.Extensions;
using Tasks.Cmd.Application.Models;

namespace Tasks.Cmd.Application.Services;

public class GroupService:IGroupService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GroupService> _logger;
    private readonly IConfiguration _configuration;

    public GroupService(HttpClient httpClient, IConfiguration configuration, ILogger<GroupService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<GroupModel> GetGroupById(Guid groupId)
    {
        TokenResponse tokenResponse;
        
        using (var client= new HttpClient())
        {
            tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = _configuration["Services:IdentityServerUrl"]+"/connect/token",
                ClientId = "taskCmd-service",
                ClientSecret = "taskCmdSecret",
                Scope = "taskCmdApi groupQueryApi"
            });
        }
        
        if (tokenResponse.IsError)
        {
            _logger.LogCritical("ошибка при получении токена");
            throw new Exception();
        }
        
        _logger.LogInformation($"token: {tokenResponse.AccessToken}");
        
        _httpClient.SetBearerToken(tokenResponse.AccessToken);
        
        var response = await _httpClient.GetAsync($"/api/Group/GetGroupById?id={groupId}");
        
        if(response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            _logger.LogCritical("ошибка авторизации при попытки обратиться к сервису");
            throw new Exception();
        }
        
        
        return await response.ReadContentAs<GroupModel>();
    }
}