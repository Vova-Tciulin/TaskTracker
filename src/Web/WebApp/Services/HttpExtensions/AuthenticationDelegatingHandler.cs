using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace WebApp.Services.HttpExtensions;


/// <summary>
/// Добавляет jwt токен в строку запроса при обращении к микросервисам 
/// </summary>
public class AuthenticationDelegatingHandler:DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthenticationDelegatingHandler> _logger;
    private readonly IConfiguration _configuration;

    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationDelegatingHandler> logger, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken =await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            _logger.LogWarning("failed to get token from httpContextAccessor");
            TokenResponse tokenResponse;
        
            using (var identityClient= new HttpClient())
            {
                tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
                {
                    Address = $"{_configuration["IdentityServerUrl"]}/connect/token",
                    ClientId = _configuration["ClientConfig:ClientId"],
                    ClientSecret = _configuration["ClientConfig:ClientSecret"],
                    Scope = _configuration["ClientConfig:Scope"],
                });
            }
        
            if (tokenResponse.IsError|| String.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                _logger.LogCritical($"failed to get token {tokenResponse.ErrorDescription}");
            }

            accessToken = tokenResponse.AccessToken;
        }
       
        request.SetBearerToken(accessToken);
        
        return await base.SendAsync(request, cancellationToken);
    }
}