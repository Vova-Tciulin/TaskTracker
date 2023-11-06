using EventBus.Messages;
using EventBus.Messages.Messages;
using MassTransit;
using RabbitMQ.Client;
using Task.Query.Api.EventBusConsumer;

namespace Task.Query.Api.Configuration;

public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransit(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMassTransit(config =>
        {
            config.AddConsumer<TasksEventConsumer>();
            config.UsingRabbitMq((context, cfg) =>
            {
        
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                cfg.UseConcurrencyLimit(1);
                cfg.ReceiveEndpoint(EventBusConstants.TaskQueryQueue, c =>
                {
                    c.ConfigureConsumeTopology = false;
                    c.ConfigureConsumer<TasksEventConsumer>(context);
            
                    c.Bind<EventMessage>(x =>
                    {
                        x.RoutingKey = EventBusConstants.TaskAllEvents;
                        x.ExchangeType = ExchangeType.Topic;
                        x.Durable = true;
                        x.AutoDelete = false;
                
                    });
            
                });
            });
        });
        
        return service;
    }
}