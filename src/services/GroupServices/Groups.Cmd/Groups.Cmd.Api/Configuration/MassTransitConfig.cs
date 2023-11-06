using EventBus.Messages;
using EventBus.Messages.Messages;
using Groups.Cmd.Api.EventBusConsumers;
using MassTransit;
using RabbitMQ.Client;

namespace Groups.Cmd.Api.Configuration;

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
        
                //Producer
                cfg.Publish<EventMessage>(x =>
                {
                    x.Durable = true; 
                    x.AutoDelete = false; 
                    x.ExchangeType = ExchangeType.Topic;
                });
        
                //Consumers
                cfg.UseConcurrencyLimit(1);
                cfg.ReceiveEndpoint(EventBusConstants.GroupCmdQueue, c =>
                {
                    c.ConfigureConsumeTopology = false;
                    c.ConfigureConsumer<TasksEventConsumer>(context);
            
                    c.Bind<EventMessage>(x =>
                    {
                        x.RoutingKey = EventBusConstants.TaskCreatedEvents;
                        x.ExchangeType = ExchangeType.Topic;
                        x.Durable = true;
                        x.AutoDelete = false;
                
                    });
            
                    c.Bind<EventMessage>(x =>
                    {
                        x.RoutingKey = EventBusConstants.TaskRemovedEvents;
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