﻿using Microsoft.AspNetCore.Authentication;

namespace WebApp.Configuration;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthentication(opt =>
        {
            opt.DefaultScheme = "Cookies";
            opt.DefaultChallengeScheme = "oidc";
    
        }).AddCookie("Cookies")
        .AddOpenIdConnect("oidc", opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.Events.OnRedirectToIdentityProvider = context =>
            {
                context.ProtocolMessage.IssuerAddress = "http://localhost:8080/connect/authorize";
                return Task.CompletedTask;
            };
    
            opt.Events.OnRedirectToIdentityProviderForSignOut = context =>
            {
                context.ProtocolMessage.IssuerAddress = "http://localhost:8080/connect/endsession";
                return Task.CompletedTask;
            };

            opt.RefreshOnIssuerKeyNotFound = true;
            opt.RefreshInterval= TimeSpan.FromSeconds(2);
    
            opt.SignInScheme = "Cookies";
            opt.Authority = configuration["IdentityServerUrl"];
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
            opt.Scope.Add("IdentityApi");
            opt.Scope.Add("nickname");
            
            opt.ClaimActions.MapUniqueJsonKey("nickname","nickname");
        });

        return service;
    }
}