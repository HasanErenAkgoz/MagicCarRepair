using Core.Packages.Application.Repositories;
using Core.Packages.Application.Security.Hashing;
using Core.Packages.Application.Security.JWT;
using Core.Packages.Application.Security.Tokens;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IDataResult<TokenResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<IDataResult<TokenResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.Repository<User>().GetAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return new ErrorDataResult<TokenResponse>("Email already exists");

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var tokenResponse = await _tokenService.CreateTokenAsync(user);
        return new SuccessDataResult<TokenResponse>(tokenResponse, "Registration successful");
    }
} 