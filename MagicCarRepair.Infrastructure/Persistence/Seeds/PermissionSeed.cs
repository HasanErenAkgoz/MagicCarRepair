using System.Reflection;
using MagicCarRepair.Application.Security.SecuredOperation;
using MagicCarRepair.Domain.Entities;

public static class PermissionSeed
{
    public static List<Permission> GetPermissions()
    {
        var permissions = new List<Permission>();
        var assembly = Assembly.Load("MagicCarRepair.Application");

        // Application katmanındaki tüm Command ve Query'leri bul
        var types = assembly.GetTypes()
            .Where(t => (t.Name.EndsWith("Command") || t.Name.EndsWith("Query")) 
                && !t.IsAbstract && !t.IsInterface);

        foreach (var type in types)
        {
            var permission = SecuredOperationAttribute.GeneratePermissionFromType(type);
            var module = type.Namespace?.Split('.').FirstOrDefault(x => x != "Features" && x != "Commands" && x != "Queries");

            permissions.Add(new Permission
            {
                Id = Guid.NewGuid(),
                Name = permission,
                Description = $"{module} modülü için {permission.Split('.').Last()} izni",
                Group = module
            });
        }

        return permissions.DistinctBy(p => p.Name).ToList();
    }
} 