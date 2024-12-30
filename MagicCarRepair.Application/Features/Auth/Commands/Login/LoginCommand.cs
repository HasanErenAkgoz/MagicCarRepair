using Core.Packages.Application.Security.Tokens;
using Core.Utilities.Results;
using MediatR;
using MagicCarRepair.Application.Security.Authorization;

namespace MagicCarRepair.Application.Features.Auth.Commands.Login;

// Login için yetkilendirme gerekmez
public class LoginCommand : IRequest<IDataResult<TokenResponse>>, IPublicRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
} 