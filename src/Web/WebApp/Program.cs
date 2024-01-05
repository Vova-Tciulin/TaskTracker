using System.IdentityModel.Tokens.Jwt;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApp.Configuration;
using WebApp.Extensions;
using WebApp.Services;
using WebApp.Services.HttpExtensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<AuthenticationDelegatingHandler>();





builder.Services.AddHealthChecks();

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddHttpClient<ITaskService, TaskService>()
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<IGroupService, GroupService>()
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
    

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

IdentityModelEventSource.ShowPII = true;

builder.Services.AddAuthenticationConfig(builder.Configuration);


builder.Services.AddTransient<ExceptionHandlingMiddleware>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = _ => true
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();

