using System;
using System.Linq;

namespace MagicCarRepair.Application.Security.SecuredOperation;

[AttributeUsage(AttributeTargets.Class)]
public class SecuredOperationAttribute : Attribute
{
    public int Priority { get; set; }
    public string[] Roles { get; private set; }

    public SecuredOperationAttribute(int priority = 1)
    {
        Priority = priority;
    }

    public static string GeneratePermissionFromType(Type type)
    {
        // CreateCarCommand -> Cars.Create
        // UpdateUserCommand -> Users.Update
        // GetCarByIdQuery -> Cars.View
        var typeName = type.Name;
        var module = type.Namespace?.Split('.')
            .FirstOrDefault(x => x != "Features" && x != "Commands" && x != "Queries");

        if (typeName.EndsWith("Command"))
        {
            var action = typeName.Replace("Command", "").Replace(module ?? "", "");
            return $"{module}.{action}";
        }
        
        if (typeName.EndsWith("Query"))
        {
            return $"{module}.View";
        }

        return typeName;
    }
} 