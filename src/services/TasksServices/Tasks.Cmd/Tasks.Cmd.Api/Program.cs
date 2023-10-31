using EventBus.Messages;
using EventBus.Messages.Messages;
using MassTransit;
using RabbitMQ.Client;
using Tasks.Cmd.Api.EventBusConsumers;
using Tasks.Cmd.Api.Extensions;
using Tasks.Cmd.Application;
using Tasks.Cmd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

//Add Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//Add MassTransit
builder.Services.AddMassTransit(u =>
{
    u.AddConsumer<GroupEventConsumer>();
    u.UsingRabbitMq((ctx, cfg) =>
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