using MassTransit;
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