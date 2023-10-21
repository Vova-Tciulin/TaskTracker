﻿using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Cmd.Infrastructure.Contracts;
using Tasks.Cmd.Infrastructure.Data;
using Tasks.Cmd.Infrastructure.EventStores;
using Tasks.Cmd.Infrastructure.Repositories;

namespace Tasks.Cmd.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskDbContext, TaskDbContext>();
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<ITaskEventRepository, TaskEventRepository>();
        
        //Add MassTransit
        services.AddMassTransit(u =>
        {
            u.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
            });
        });
        
        return services;
    }
}