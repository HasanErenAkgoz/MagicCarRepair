using Core.Packages.Application.Repositories;
using Core.Packages.Application.Security;
using Core.Packages.Application.Security.Hashing;
using Core.Packages.Application.Security.JWT;
using Core.Packages.Application.Security.Tokens;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepair.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<TokenResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<IDataResult<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email == request.Email && u.IsActive,
                include: q => q.Include(u => u.UserRoles)
                              .ThenInclude(ur => ur.Role));

        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return new ErrorDataResult<TokenResponse>("Invalid email or password");

        var tokenResponse = await _tokenService.CreateTokenAsync(user);
        return new SuccessDataResult<TokenResponse>(tokenResponse, "Login successful");
    }
} 