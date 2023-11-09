using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Config;

public static class IdentityConfig
{
    //claims для пользователей, чтобы предоставить определенные данные о них 
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    //области, к которым имеют возможность обратиться клиенты
    public static IEnumerable<ApiScope> GetApiScope() =>
        new List<ApiScope>()
        {
            new ApiScope("taskQueryApi", "Task query service api"),
            new ApiScope("taskCmdApi","Task command api"),
            new ApiScope("groupQueryApi","Group query api"),
            new ApiScope("groupCmdApi","Group command api")
        };

    //Содержит созданные ранее области для сервисов.
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>()
        {
            new ApiResource("taskQueryApi", "Task query service api")
            {
                Scopes = { "taskQueryApi" }
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
            }
        };
    
    //микросервисы
    public static IEnumerable<Client> GetClients() =>
        new List<Client>()
        {
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
                    "groupCmdApi"
                }
            },
            new Client()
            {
                ClientName = "MVC Client",
                ClientId = "mvc-client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "taskQueryApi",
                    "taskCmdApi",
                    "groupQueryApi",
                    "groupCmdApi"
                },
                ClientSecrets = { new Secret("MVCSecret".Sha256()) },
            }
        };
}