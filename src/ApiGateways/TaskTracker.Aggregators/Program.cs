using TaskTracker.Aggregators.HttpClientHandlers;
using TaskTracker.Aggregators.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = builder.Configuration["Services:IdentityServerUrl"];
        opt.Audience = "aggregatorsApi";
    });
builder.Services.AddScoped<AuthenticationDelegatingHandler>();
builder.Services.AddHttpClient<IGroupService, GroupService>(o =>
    {
        o.BaseAddress = new Uri(builder.Configuration["Services:GroupQueryUrl"]);
    })
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
builder.Services.AddHttpClient<ITaskService, TaskService>(o =>
    {
        o.BaseAddress = new Uri(builder.Configuration["Services:TaskQueryUrl"]);
    })
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

var app = builder.Build();



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