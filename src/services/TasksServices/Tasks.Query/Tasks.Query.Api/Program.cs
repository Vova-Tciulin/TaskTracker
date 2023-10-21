using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

//Add Masstransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<TasksEventConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint("tasksEvent-queue", c =>
        {
            c.ConfigureConsumer<TasksEventConsumer>(ctx);
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