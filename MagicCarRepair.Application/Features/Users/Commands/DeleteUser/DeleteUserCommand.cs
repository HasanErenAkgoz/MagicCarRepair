using Core.Packages.Application.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<IResult>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { "users.delete" };
} 