using EventBus.Messages;
using EventBus.Messages.Messages;
using Groups.Query.Api.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Groups.Query.Api.Configuration;

public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransit(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMassTransit(config =>
        {
            config.AddConsumer<GroupsEventConsumer>();
            
            config.UsingRabbitMq((context, cfg) =>
            {
        
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                cfg.UseConcurrencyLimit(1);
                
                //consumers 
                cfg.ReceiveEndpoint(EventBusConstants.GroupQueryQueue, c =>
                {
                    c.ConfigureConsumeTopology = false;
                    c.ConfigureConsumer<GroupsEventConsumer>(context);
            
                    c.Bind<EventMessage>(x =>
                    {
                        x.RoutingKey = EventBusConstants.GroupAllEvents;
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