using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Groups.Cmd.Application.HttpHandlers;

/// <summary>
/// Добавляет jwt токен в строку запроса при обращении к микросервисам 
/// </summary>
public class AuthenticationDelegatingHandler:DelegatingHandler
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationDelegatingHandler> _logger;

    public AuthenticationDelegatingHandler(IConfiguration configuration, ILogger<AuthenticationDelegatingHandler> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        TokenResponse tokenResponse;
        
        using (var client= new HttpClient())
        {
            tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = _configuration["Services:IdentityServerUrl"]+"/connect/token",
                ClientId = "groupCmd-service",
                ClientSecret = "groupCmdSecret",
                Scope = "IdentityApi"
            });
        }
        
        if (tokenResponse.IsError|| String.IsNullOrEmpty(tokenResponse.AccessToken))
        {
            _logger.LogCritical("ошибка при получении токена");
            throw new Exception();
        }
        
        _logger.LogInformation($"token: {tokenResponse.AccessToken}");
        request.SetBearerToken(tokenResponse.AccessToken);
        
        return await base.SendAsync(request, cancellationToken);
    }
    
}