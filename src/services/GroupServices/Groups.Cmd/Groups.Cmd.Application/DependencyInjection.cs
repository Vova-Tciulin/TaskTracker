using System.Reflection;
using FluentValidation;
using Groups.Cmd.Application.Behaviours;
using Groups.Cmd.Application.EventSourcingHandlers;
using Groups.Cmd.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Groups.Cmd.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IEventSourcingHandler<GroupAggregate>, EventSourcingHandler>();
        
        return services;
    }
}