using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Query.Infrastructure.Data;
using Tasks.Query.Infrastructure.Repositories;

namespace Tasks.Query.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(o =>
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<ITaskRepository, TaskRepository>();
        
        
        return services;
    }
}