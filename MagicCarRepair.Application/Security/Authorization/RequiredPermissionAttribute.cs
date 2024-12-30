namespace MagicCarRepair.Application.Security.Authorization;

[AttributeUsage(AttributeTargets.Class)]
public class RequiredPermissionAttribute : Attribute
{
    public Type CommandType { get; }
    public string[] AlternativePermissions { get; }

    public RequiredPermissionAttribute(Type commandType, params string[] alternativePermissions)
    {
        CommandType = commandType;
        AlternativePermissions = alternativePermissions;
    }
} 