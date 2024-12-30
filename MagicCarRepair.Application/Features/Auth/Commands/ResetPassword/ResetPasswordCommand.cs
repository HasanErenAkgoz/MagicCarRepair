using Core.Utilities.Results;
using MediatR;
using MagicCarRepair.Application.Security.Authorization;

namespace MagicCarRepair.Application.Features.Auth.Commands.ResetPassword;

// Şifre sıfırlama için yetkilendirme gerekir ama esnek olabilir
public class ResetPasswordCommand : IRequest<IResult>, ISecuredRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
    
    // Eğer kullanıcı authenticated ise işleme devam et
    public bool BypassAuthorization => true;
} 