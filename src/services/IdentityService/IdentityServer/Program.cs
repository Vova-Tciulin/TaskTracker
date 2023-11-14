using IdentityServer.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(opt =>
    {
        opt.IssuerUri = "http://localhost:8080";
        
    })
    .AddInMemoryApiScopes(IdentityConfig.GetApiScope())
    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
    .AddTestUsers(IdentityConfig.GetUsers())
    .AddInMemoryClients(IdentityConfig.GetClients(builder.Configuration))
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();