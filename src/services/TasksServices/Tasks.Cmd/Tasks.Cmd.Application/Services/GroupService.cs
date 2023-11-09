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
    

    public GroupService(HttpClient httpClient, ILogger<GroupService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<GroupModel> GetGroupById(Guid groupId)
    {
        
        var response = await _httpClient.GetAsync($"/api/Group/GetGroupById?id={groupId}");
        
        if(response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            _logger.LogCritical("ошибка авторизации при попытки обратиться к сервису");
            throw new Exception();
        }
        
        return await response.ReadContentAs<GroupModel>();
    }
}