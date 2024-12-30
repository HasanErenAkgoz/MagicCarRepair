using Core.Packages.Domain.Identity;
using Core.Packages.Domain.Security;

public static class RoleSeed
{
    public static List<Role> GetRoles(List<Permission> permissions)
    {
        if (permissions == null || !permissions.Any())
            throw new InvalidOperationException("No permissions found to seed");

        return new List<Role>
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Description = "Sistem Yöneticisi",
                IsActive = true,
                RolePermissions = permissions.Select(p => new RolePermission 
                { 
                    Id = Guid.NewGuid(),
                    PermissionId = p.Id 
                }).ToList()
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "User",
                Description = "Standart Kullanıcı",
                IsActive = true
            }
        };
    }
} 