using System.IdentityModel.Tokens.Jwt;
using EventBus.Messages;
using EventBus.Messages.Messages;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using Serilog;
using Tasks.Cmd.Api.Configuration;
using Tasks.Cmd.Api.EventBusConsumers;
using Tasks.Cmd.Api.Extensions;
using Tasks.Cmd.Application;
using Tasks.Cmd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"], "TaskEventDb Health",
        HealthStatus.Degraded);

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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = builder.Configuration["Services:IdentityServerUrl"];
        opt.Audience = "taskCmdApi";
    });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = _ => true
});

app.Run();