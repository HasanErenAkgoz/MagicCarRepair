using Core.Packages.Application.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Roles.Commands.UpdateRolePermissions;

public class UpdateRolePermissionsCommand : IRequest<IResult>, ISecuredRequest
{
    public Guid RoleId { get; set; }
    public List<Guid> PermissionIds { get; set; }

    public string[] Roles => new[] { "roles.manage.permissions" };
    public bool BypassAuthorization => false;
} 