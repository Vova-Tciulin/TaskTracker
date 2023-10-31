using EventBus.Messages;
using EventBus.Messages.Messages;
using Groups.Query.Api.Consumers;
using Groups.Query.Application;
using Groups.Query.Infrastructure;
using MassTransit;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

//Add massTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<GroupsEventConsumer>();
    config.UsingRabbitMq((context, cfg) =>
    {
        
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.UseConcurrencyLimit(1);
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

builder.Services.AddScoped<GroupsEventConsumer>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();