using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Cmd.Application.Behaviours;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Application.Services;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton(configuration);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IEventSourcingHandler<TaskAggregate>, EventSourcingHandler>();
        //services.AddScoped<IGroupService, GroupService>();
        
        services.AddHttpClient<IGroupService, GroupService>(o =>
            o.BaseAddress = new Uri(configuration["Services:GroupQueryUrl"]));
        
        return services;
    }
}