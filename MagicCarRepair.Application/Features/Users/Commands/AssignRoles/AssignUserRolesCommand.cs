using Core.Utilities.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Commands.AssignRoles;

public class AssignUserRolesCommand : IRequest<IResult>
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; } = new();
} 