using EventBus.Messages;
using EventBus.Messages.Messages;
using Groups.Cmd.Api.EventBusConsumers;
using Groups.Cmd.Api.Extensions;
using Groups.Cmd.Application;
using Groups.Cmd.Infrastructure;
using MassTransit;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Add Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//Add massTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<TasksEventConsumer>();
    
    config.UsingRabbitMq((context, cfg) =>
    {
        
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        
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


var app = builder.Build();


app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();