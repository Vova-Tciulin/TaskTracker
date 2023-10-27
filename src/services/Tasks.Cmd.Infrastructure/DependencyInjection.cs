﻿using System.Reflection;
using EventBus.Messages.Messages;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Tasks.Cmd.Domain.Aggregates;
using Tasks.Cmd.Infrastructure.Contracts;
using Tasks.Cmd.Infrastructure.Data;
using Tasks.Cmd.Infrastructure.EventStores;
using Tasks.Cmd.Infrastructure.Producers;
using Tasks.Cmd.Infrastructure.Repositories;

namespace Tasks.Cmd.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskDbContext, TaskDbContext>();
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<ITaskEventRepository, TaskEventRepository>();
        services.AddScoped<IProducer, Producer>();
        
        //Add MassTransit
        services.AddMassTransit(u =>
        {
            u.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                cfg.Publish<EventMessage>(x =>
                {
                    x.Durable = true; 
                    x.AutoDelete = false; 
                    x.ExchangeType = ExchangeType.Topic;
                });
            });
        });
        
        return services;
    }
}