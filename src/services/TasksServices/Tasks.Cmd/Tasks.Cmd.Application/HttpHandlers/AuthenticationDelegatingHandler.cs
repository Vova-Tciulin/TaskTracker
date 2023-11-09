using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Tasks.Cmd.Application.HttpHandlers;

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
                ClientId = "taskCmd-service",
                ClientSecret = "taskCmdSecret",
                Scope = "taskCmdApi groupQueryApi"
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