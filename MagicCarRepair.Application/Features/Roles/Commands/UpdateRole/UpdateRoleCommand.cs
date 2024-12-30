using Core.Utilities.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Guid> PermissionIds { get; set; } = new();
} 