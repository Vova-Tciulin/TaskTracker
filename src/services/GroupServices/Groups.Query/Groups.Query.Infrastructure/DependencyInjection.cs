using Groups.Query.Infrastructure.Data;
using Groups.Query.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Groups.Query.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GroupDbContext>(o =>
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IGroupRepository, GroupRepository>();
        
        return services;
    }
}