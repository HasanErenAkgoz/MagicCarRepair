using System.Reflection;
using Core.Packages.Domain.Identity;
using Core.Packages.Domain.Security;
using Core.Packages.Infrastructure.Persistence;
using MagicCarRepair.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Permission = MagicCarRepair.Domain.Entities.Permission;

namespace MagicCarRepair.Infrastructure.Persistence;

public class MagicCarRepairDbContext : BaseDbContext
{
    public MagicCarRepairDbContext(DbContextOptions<MagicCarRepairDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Core.Packages'tan gelen entity'lerin konfigürasyonları
        base.OnModelCreating(modelBuilder);
    }
} 