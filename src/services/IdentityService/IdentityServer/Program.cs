using IdentityServer.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(opt =>
    {
        opt.IssuerUri = builder.Configuration["IdentityServer"];
    })
    .AddInMemoryApiScopes(IdentityConfig.GetApiScope())
    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
    .AddInMemoryClients(IdentityConfig.GetClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();