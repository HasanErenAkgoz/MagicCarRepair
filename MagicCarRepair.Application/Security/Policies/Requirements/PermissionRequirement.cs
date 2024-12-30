using Microsoft.AspNetCore.Authorization;

namespace MagicCarRepair.Application.Security.Policies.Requirements;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
} 