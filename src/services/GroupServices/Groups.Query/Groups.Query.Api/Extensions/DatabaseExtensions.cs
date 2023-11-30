using Groups.Query.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Groups.Query.Api.Extensions;

public class DatabaseExtensions
{
    public static void MigrateDatabase(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<GroupDbContext>();

            int maxAttempts = 3;
            int attemptCount = 0;
            var delay = 5;

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
        }
    }
}