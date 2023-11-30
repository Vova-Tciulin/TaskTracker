using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;
public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<IdentityDb>();
            
            int maxAttempts = 3;
            int attemptCount = 0;
            int delay = 5;
            
            while (attemptCount < maxAttempts)
            {
                try
                {
                    Log.Information($"Attempt {attemptCount+1} to make a database migration");
                    context.Database.Migrate();
                    break;
                }
                catch (Exception e)
                {
                    Log.Error("A database migration error occurred");

                    attemptCount++;
                    Task.Delay(TimeSpan.FromSeconds(delay)).Wait();
                    delay += 5;
                }
            }

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var alice = userMgr.FindByNameAsync("AliceSmith@email.com").Result;
            if (alice == null)
            {
                alice = new User
                {
                    UserName = "AliceSmith@email.com",
                    Email = "AliceSmith@email.com",
                    NickName = "AliceNickName",
                    FirstName = "Alice",
                    LastName = "Smith",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Alice"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.NickName, "AliceNickName"),
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }
            var bob = userMgr.FindByNameAsync("BobJ@email.com").Result;
            if (bob == null)
            {
                bob = new User
                {
                    Id = "dc06278d-a0fa-47db-b885-dc94390f381a",
                    UserName = "BobJ@email.com",
                    Email = "BobJ@email.com",
                    NickName = "BobNickName",
                    FirstName = "Bob",
                    LastName = "J",
                    EmailConfirmed = true,
                };
                
                var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Bob"),
                    new Claim(JwtClaimTypes.Email, "BobJ@email.com"),
                    new Claim(JwtClaimTypes.FamilyName, "J"),
                    new Claim(JwtClaimTypes.NickName, "BobNickName"),
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("bob created");
            }
            else
            {
                Log.Debug("bob already exists");
            }

            
        }
    }
}