using Core.Packages.Application.Security.Tokens;
using Core.Utilities.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Auth.Commands.Register;

// Register için yetkilendirme gerekmez
public class RegisterCommand : IRequest<IDataResult<TokenResponse>>, IPublicRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
} 