using Core.Utilities.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<IResult>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Guid> PermissionIds { get; set; } = new();
} 