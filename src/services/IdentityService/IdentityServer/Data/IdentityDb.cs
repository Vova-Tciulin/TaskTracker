using IdentityServer.Data.Configurations;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class IdentityDb:IdentityDbContext<User>
{
    public IdentityDb(DbContextOptions opt)
        :base(opt)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfig());
    }
}