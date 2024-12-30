using Core.Utilities.Results;
using MagicCarRepair.Application.Security.Authorization;
using MediatR;

namespace MagicCarRepair.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<IResult>, ISecuredRequest
{
    public string Email { get; set; }
    
    // Eğer kullanıcı authenticated ise işleme devam et
    public bool BypassAuthorization => true;
} 