namespace MagicCarRepair.Application.Security.Authorization;

[AttributeUsage(AttributeTargets.Class)]
public class RequiredRolesAttribute : Attribute
{
    public string[] Roles { get; }

    public RequiredRolesAttribute(params string[] roles)
    {
        Roles = roles;
    }
} 