using IdentityServer;
using IdentityServer.Config;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

//Add serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

//Add IdentityDb
builder.Services.AddDbContext<IdentityDb>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(opt =>
    {
        opt.Password.RequiredLength = 7;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<IdentityDb>();

//Add IdentityServer
builder.Services.AddIdentityServer(opt =>
    {
        opt.IssuerUri = "http://localhost:8080";
        
    })
    .AddInMemoryApiScopes(IdentityConfig.GetApiScope())
    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
    .AddAspNetIdentity<User>()
    .AddInMemoryClients(IdentityConfig.GetClients(builder.Configuration))
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

SeedData.EnsureSeedData(app);

app.Run();