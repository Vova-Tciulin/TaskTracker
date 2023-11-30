using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApp.Services;
using WebApp.Services.HttpExtensions;
using WebApp.Services.Moq;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<AuthenticationDelegatingHandler>();


/*
builder.Services.AddScoped<Db>();
builder.Services.AddScoped<IGroupService, GroupServiceMoq>();
builder.Services.AddScoped<ITaskService, TaskServiceMoq>();
*/


builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddHttpClient<ITaskService, TaskService>()
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient<IGroupService, GroupService>()
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
    

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

IdentityModelEventSource.ShowPII = true;

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = "Cookies";
        opt.DefaultChallengeScheme = "oidc";
        
    }).AddCookie("Cookies")
    .AddOpenIdConnect("oidc", opt =>
    {
        opt.RequireHttpsMetadata = false;
        
        opt.Events.OnRedirectToIdentityProvider = context =>
        {
            // Intercept the redirection so the browser navigates to the right URL in your host
            context.ProtocolMessage.IssuerAddress = "http://localhost:8080/connect/authorize";
            return Task.CompletedTask;
        };

        
        opt.SignInScheme = "Cookies";
        opt.Authority = builder.Configuration["IdentityServerUrl"];
        opt.ClientId = "mvc-client";
        opt.ResponseType = "code id_token";
        opt.SaveTokens = true;
        opt.ClientSecret = "MVCSecret";
        opt.GetClaimsFromUserInfoEndpoint = true;
        
        opt.ClaimActions.DeleteClaim("sid");
        opt.ClaimActions.DeleteClaim("idp");
        
        opt.Scope.Add("taskQueryApi");
        opt.Scope.Add("groupQueryApi");
        opt.Scope.Add("taskCmdApi");
        opt.Scope.Add("groupCmdApi");
        opt.Scope.Add("aggregatorsApi");
    });
    
    


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();