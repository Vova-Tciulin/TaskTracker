using System.Reflection;
using Groups.Query.Application.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = Groups.Query.Application.EventHandlers.EventHandler;


namespace Groups.Query.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IEventHandler, EventHandler>();
        
        return services;
    }
}