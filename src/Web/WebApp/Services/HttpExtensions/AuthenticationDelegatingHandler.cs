using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace WebApp.Services.HttpExtensions;

public class AuthenticationDelegatingHandler:DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthenticationDelegatingHandler> _logger;

    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationDelegatingHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
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
                    Address = "http://identityserver:80/connect/token",
                    ClientId = "mvc-client",
                    ClientSecret = "MVCSecret",
                    Scope = "taskQueryApi taskCmdApi groupQueryApi groupCmdApi aggregatorsApi",
                });
            }
        
            if (tokenResponse.IsError|| String.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                _logger.LogCritical($"ошибка при получении токена {tokenResponse.ErrorDescription}");
            }

            accessToken = tokenResponse.AccessToken;
        }
       
        request.SetBearerToken(accessToken);
        
        return await base.SendAsync(request, cancellationToken);
    }
}