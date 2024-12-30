using Core.Packages.Application.Repositories;
using Core.Packages.Application.Security.Hashing;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public ResetPasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<IResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email == request.Email && 
                          u.PasswordResetToken == request.Token && 
                          u.PasswordResetTokenExpires > DateTime.UtcNow);

        if (user == null)
            return new ErrorResult("Invalid or expired password reset token");

        user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpires = null;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SuccessResult("Password has been reset successfully");
    }
} 