using MagicCarRepair.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepair.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(p => p.Description)
            .HasMaxLength(250);
            
        builder.Property(p => p.Group)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(p => p.Name)
            .IsUnique();
            
        builder.HasIndex(p => p.Group);
    }
} 