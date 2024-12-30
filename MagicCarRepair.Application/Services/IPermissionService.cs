using Core.Packages.Domain.Security;

public interface IPermissionService
{
    Task<List<Permission>> GetAllPermissionsAsync();
    Task<List<Permission>> GetRolePermissionsAsync(Guid roleId);
    Task AssignPermissionToRoleAsync(Guid roleId, Guid permissionId);
    Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);
} 