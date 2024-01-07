using TaskTracker.Aggregators.HttpClientHandlers;
using TaskTracker.Aggregators.Services.Group;
using TaskTracker.Aggregators.Services.Identity;
using TaskTracker.Aggregators.Services.Task;

namespace TaskTracker.Aggregators.Configuration;

/// <summary>
/// добавляет сервисы httpClient и обработчик аутентификации 
/// </summary>
public static class HttpClientConfig
{
    public static IServiceCollection AddHttpClientConfig(this IServiceCollection services, IConfiguration configuration)
    {
           
        services.AddScoped<AuthenticationDelegatingHandler>();
        
        // httpclient для groups.query сервиса
        services.AddHttpClient<IGroupService, GroupService>(o =>
            {
                o.BaseAddress = new Uri(configuration["Services:GroupQueryUrl"]);
            })
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        // httpclient для task.query сервиса
        services.AddHttpClient<ITaskService, TaskService>(o =>
            {
                o.BaseAddress = new Uri(configuration["Services:TaskQueryUrl"]);
            })
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        // httpclient для identity сервиса
        services.AddHttpClient<IIdentityService, IdentityService>(o =>
            {
                o.BaseAddress = new Uri(configuration["Services:IdentityServerUrl"]);
            })
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        return services;
    }
}