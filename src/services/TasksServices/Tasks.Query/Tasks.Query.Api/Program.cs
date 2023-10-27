using EventBus.Messages;
using EventBus.Messages.Messages;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Task.Query.Api.EventBusConsumer;
using Task.Query.Api.Extensions;
using Tasks.Query.Application;
using Tasks.Query.Infrastructure;
using Tasks.Query.Infrastructure.Data;

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
    config.AddConsumer<TasksEventConsumer>();
    config.UsingRabbitMq((context, cfg) =>
    {
        
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
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

builder.Services.AddScoped<TasksEventConsumer>();

//Add middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

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