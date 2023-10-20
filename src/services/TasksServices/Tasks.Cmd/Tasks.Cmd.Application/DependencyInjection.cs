using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Cmd.Application.Behaviours;
using Tasks.Cmd.Application.EventSourcingHandlers;
using Tasks.Cmd.Domain.Aggregates;

namespace Tasks.Cmd.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IEventSourcingHandler<TaskAggregate>, EventSourcingHandler>();
        
        return services;
    }
}