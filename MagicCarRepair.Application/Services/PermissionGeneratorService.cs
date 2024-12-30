using System;
using System.Collections.Generic;
using System.Reflection;
using MagicCarRepair.Application.Security.Constants;
using MagicCarRepair.Domain.Entities;

public interface IPermissionGeneratorService
{
    List<Permission> GeneratePermissions();
}

public class PermissionGeneratorService : IPermissionGeneratorService
{
    public List<Permission> GeneratePermissions()
    {
        var permissions = new List<Permission>();
        var constantsType = typeof(PermissionConstants);
        
        foreach (var nestedType in constantsType.GetNestedTypes())
        {
            var group = nestedType.Name;
            
            foreach (var field in nestedType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.IsLiteral && !field.IsInitOnly)
                {
                    permissions.Add(new Permission
                    {
                        Id = Guid.NewGuid(),
                        Name = field.GetValue(null).ToString(),
                        Description = $"{group} için {field.Name} izni",
                        Group = group
                    });
                }
            }
        }

        return permissions;
    }
} 