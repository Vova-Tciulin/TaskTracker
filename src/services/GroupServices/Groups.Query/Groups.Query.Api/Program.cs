using Groups.Query.Api.Configuration;
using Groups.Query.Api.Consumers;
using Groups.Query.Api.Extensions;
using Groups.Query.Application;
using Groups.Query.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);

//Add layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

//Add Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//Add massTransit
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddScoped<GroupsEventConsumer>();

//Add serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = builder.Configuration["Services:IdentityServerUrl"];
        opt.Audience = "groupQueryApi";
    });

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

//Migrate Db
DatabaseExtensions.MigrateDatabase(app);

app.Run();