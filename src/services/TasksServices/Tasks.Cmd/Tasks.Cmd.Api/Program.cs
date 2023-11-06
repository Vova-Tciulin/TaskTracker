using EventBus.Messages;
using EventBus.Messages.Messages;
using MassTransit;
using RabbitMQ.Client;
using Serilog;
using Tasks.Cmd.Api.Configuration;
using Tasks.Cmd.Api.EventBusConsumers;
using Tasks.Cmd.Api.Extensions;
using Tasks.Cmd.Application;
using Tasks.Cmd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Layers
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

//Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

//Add Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//Add serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

//Add MassTransit
builder.Services.AddMassTransit(builder.Configuration);


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