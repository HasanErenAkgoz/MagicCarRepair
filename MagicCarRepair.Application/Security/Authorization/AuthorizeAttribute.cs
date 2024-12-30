namespace MagicCarRepair.Application.Security.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute
{
    public string Roles { get; set; } = string.Empty;
    public string Policy { get; set; } = string.Empty;
} 