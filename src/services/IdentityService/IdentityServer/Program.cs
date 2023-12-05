using IdentityServer;
using IdentityServer.Config;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddControllers();

//Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority ="http://identityserver:80";
        opt.Audience = "IdentityApi";
    });



var app = builder.Build();

app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

SeedData.EnsureSeedData(app);

app.Run();

