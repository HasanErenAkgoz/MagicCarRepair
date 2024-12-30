using Core.Packages.Application.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<IResult>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string[] Roles => new[] { "users.update" };
} 