using Core.Packages.Application.Results;
using Core.Packages.Application.Requests;
using MagicCarRepair.Application.Features.Users.Dtos;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Queries.GetUserList;

public class GetUserListQuery : IRequest<IDataResult<IList<UserDto>>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "users.view" };
    public bool BypassAuthorization => false;

    public GetUserListQuery()
    {
        PageRequest = new PageRequest();
    }
} 