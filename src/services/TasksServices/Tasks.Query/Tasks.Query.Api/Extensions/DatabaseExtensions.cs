using Microsoft.EntityFrameworkCore;
using Serilog;
using Tasks.Query.Infrastructure.Data;

namespace Task.Query.Api.Extensions;

public class DatabaseExtensions
{
    public static void MigrateDatabase(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<TaskDbContext>();

            int maxAttempts = 3;
            int attemptCount = 0;

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
                    System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                }
            }
        }
    }
}