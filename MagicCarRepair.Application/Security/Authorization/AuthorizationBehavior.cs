using System.Reflection;
using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using MagicCarRepair.Application.Interfaces;
using MagicCarRepair.Application.Security.SecuredOperation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepair.Application.Security.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizationBehavior(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var handlerType = typeof(TRequest).GetTypeInfo();
        var securedOperation = handlerType.GetCustomAttribute<SecuredOperationAttribute>();

        if (securedOperation != null)
        {
            var permission = SecuredOperationAttribute.GeneratePermissionFromType(typeof(TRequest));
            
            var hasPermission = await CheckPermissions(new[] { permission });
            if (!hasPermission)
                throw new UnauthorizedAccessException("You don't have permission");
        }

        return await next();
    }

    private async Task<bool> CheckPermissions(string[] requiredRoles)
    {
        if (!_currentUserService.IsAuthenticated)
            return false;

        var user = await _unitOfWork.Repository<User>()
            .GetAsync(
                u => u.Id == _currentUserService.UserId && u.IsActive,
                include: q => q.Include(u => u.UserRoles)
                              .ThenInclude(ur => ur.Role)
                              .ThenInclude(r => r.RolePermissions
                                  .Where(rp => requiredRoles.Contains(rp.Permission.Name)))
                                  .ThenInclude(rp => rp.Permission));

        if (user == null)
            return false;

        return user.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions)
            .Any(rp => requiredRoles.Contains(rp.Permission.Name));
    }
} 