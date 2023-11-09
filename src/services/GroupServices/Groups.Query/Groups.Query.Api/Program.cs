using Groups.Query.Api.Configuration;
using Groups.Query.Api.Consumers;
using Groups.Query.Api.Extensions;
using Groups.Query.Application;
using Groups.Query.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

//Add Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//Add massTransit
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddScoped<GroupsEventConsumer>();

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

app.Run();