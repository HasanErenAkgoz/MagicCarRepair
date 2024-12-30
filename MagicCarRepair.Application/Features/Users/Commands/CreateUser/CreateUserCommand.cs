using Core.Packages.Application.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<IResult>, ISecuredRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

} 