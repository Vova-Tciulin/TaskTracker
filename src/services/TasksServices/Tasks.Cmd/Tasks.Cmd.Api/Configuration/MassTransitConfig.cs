using EventBus.Messages;
using EventBus.Messages.Messages;
using MassTransit;
using MassTransit.MultiBus;
using RabbitMQ.Client;
using Tasks.Cmd.Api.EventBusConsumers;

namespace Tasks.Cmd.Api.Configuration;

public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransit(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMassTransit(u =>
        {
            u.AddConsumer<GroupEventConsumer>();
            u.UsingRabbitMq((ctx, cfg) =>
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
                cfg.ReceiveEndpoint(EventBusConstants.TaskCmdQueue, c =>
                {
                    c.ConfigureConsumeTopology = false;
                    c.ConfigureConsumer<GroupEventConsumer>(ctx);
            
                    c.Bind<EventMessage>(x =>
                    {
                        x.RoutingKey = EventBusConstants.GroupRemovedEvents;
                        x.ExchangeType = ExchangeType.Topic;
                        x.Durable = true;
                        x.AutoDelete = false;
                    });
                    
                    c.Bind<EventMessage>(x =>
                    {
                        x.RoutingKey = EventBusConstants.GroupRemovedUserEvents;
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