using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Query.Application.EventHandlers;
using EventHandler = Tasks.Query.Application.EventHandlers.EventHandler;

namespace Tasks.Query.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IEventHandlers, EventHandler>();
        
        return services;
    }
}