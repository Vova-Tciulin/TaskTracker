﻿using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace IdentityServer.Config;

public static class IdentityConfig
{
    //claims для пользователей, чтобы предоставить определенные данные о них 
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("nickname", new []{"nickname"})
        };
    
    //области, к которым имеют возможность обратиться клиенты
    public static IEnumerable<ApiScope> GetApiScope() =>
        new List<ApiScope>()
        {
            new ApiScope("taskQueryApi", "Task query service api"),
            new ApiScope("IdentityApi", "identity api controllers"),
            new ApiScope("taskCmdApi","Task command api"),
            new ApiScope("groupQueryApi","Group query api"),
            new ApiScope("groupCmdApi","Group command api"),
            new ApiScope("aggregatorsApi", "TaskTracker aggregators Api")
        };

    //Содержит созданные ранее области для сервисов.
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>()
        {
            new ApiResource("taskQueryApi", "Task query service api")
            {
                Scopes = { "taskQueryApi" }
            },
            new ApiResource("IdentityApi", "identity api controllers")
            {
                Scopes = { "IdentityApi" }
            },
            new ApiResource("taskCmdApi", "Task command api")
            {
                Scopes = { "groupQueryApi", "taskCmdApi" }
            },
            new ApiResource("groupQueryApi", "Group query api")
            {
                Scopes = { "groupQueryApi"}
            },
            new ApiResource("groupCmdApi", "Group command api")
            {
                Scopes = { "groupCmdApi" }
            },
            new ApiResource("aggregatorsApi", "TaskTracker aggregators Api")
            {
                Scopes = {"aggregatorsApi"}
            }
        };
    
    //микросервисы
    public static IEnumerable<Client> GetClients(IConfiguration configuration) =>
        new List<Client>()
        {
            new Client
            {
                ClientId = "identity-service",
                ClientSecrets = new[] { new Secret("identitySecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = 
                {
                    "openId",
                    "IdentityApi"
                }
            },
            new Client
            {
                ClientId = "taskQuery-service",
                ClientSecrets = new[] { new Secret("taskQuerySecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = 
                {
                    "openId",
                    "taskQueryApi"
                }
            },
            new Client()
            {
                ClientId = "taskCmd-service",
                ClientSecrets = new[] { new Secret("taskCmdSecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes =
                {
                    "openId",
                    "groupQueryApi",
                    "taskCmdApi"
                }
            },
            new Client
            {
                ClientId = "groupQuery-service",
                ClientSecrets = new[] { new Secret("groupQuerySecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = 
                {
                    "openId",
                    "groupQueryApi"
                }
            },
            new Client()
            {
                ClientId = "groupCmd-service",
                ClientSecrets = new[] { new Secret("groupCmdSecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes =
                {
                    "openId",
                    "groupCmdApi",
                    "IdentityApi"
                }
            },
            new Client()
            {
                ClientName = "MVC Client",
                ClientId = "mvc-client",
                AllowedGrantTypes = new[] { "hybrid", "client_credentials" },
                RedirectUris = new List<string>{ $"{configuration["WebClientUrl"]}/signin-oidc" },
                PostLogoutRedirectUris = new List<string>{ $"{configuration["WebClientUrl"]}/signout-callback-oidc"},
                RequirePkce = false,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "taskQueryApi",
                    "taskCmdApi",
                    "groupQueryApi",
                    "groupCmdApi",
                    "aggregatorsApi",
                    "IdentityApi",
                    "nickname"
                },
                ClientSecrets = { new Secret("MVCSecret".Sha256()) },
            },
            new Client()
            {
                ClientName = "TaskTracker Aggregators",
                ClientId = "taskTracker.aggregators",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "taskQueryApi",
                    "groupQueryApi",
                    "aggregatorsApi",
                    "IdentityApi"
                },
                ClientSecrets = { new Secret("aggregatorsSecret".Sha256()) },
            },
            new Client()
            {
                ClientName = "TestClient",
                ClientId = "test-client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                RequirePkce = false,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "taskQueryApi",
                    "taskCmdApi",
                    "groupQueryApi",
                    "groupCmdApi",
                    "aggregatorsApi",
                    "IdentityApi"
                },
                ClientSecrets = { new Secret("MVCSecret".Sha256()) },
            }
        };
}