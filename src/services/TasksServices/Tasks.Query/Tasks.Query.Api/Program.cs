using EventBus.Messages;
using EventBus.Messages.Messages;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Task.Query.Api.Configuration;
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
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddScoped<TasksEventConsumer>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = builder.Configuration["Services:IdentityServerUrl"];
        opt.Audience = "taskQueryApi";
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();