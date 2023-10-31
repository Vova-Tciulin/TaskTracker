using EventBus.Messages.Messages;
using Groups.Cmd.Infrastructure.Data;
using Groups.Cmd.Infrastructure.EventStore;
using Groups.Cmd.Infrastructure.Producers;
using Groups.Cmd.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Groups.Cmd.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGroupDbContext, GroupDbContext>();
        services.AddScoped<IEventStore, EventStore.EventStore>();
        services.AddScoped<IGroupEventRepository, GroupEventRepository>();
        services.AddScoped<IProducer, Producer>();
        
        
        
        return services;
    }
}