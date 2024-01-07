using System.Reflection;
using FluentValidation;
using Groups.Cmd.Application.Behaviours;
using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Application.HttpHandlers;
using Groups.Cmd.Application.Services;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Groups.Cmd.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        
        services.AddScoped<IEventSourcingHandler<GroupAggregate>, EventSourcingHandler>();
        
        services.AddScoped<AuthenticationDelegatingHandler>();
        services.AddHttpClient<IIdentityService, IdentityService>(o =>
            {
                o.BaseAddress = new Uri(configuration["Services:IdentityServerUrl"]);
            })
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
        
        return services;
    }
}