using Core.Utilities.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Roles.Commands.AssignRoleToUser;

public class AssignRoleToUserCommand : IRequest<IResult>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public bool BypassAuthorization => false;
} 