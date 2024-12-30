using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using MagicCarRepair.Application.Interfaces;
using MagicCarRepair.Application.Security.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepair.Application.Security.Policies.Handlers;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public PermissionHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Id == _currentUserService.UserId,
                include: q => q.Include(u => u.UserRoles)
                              .ThenInclude(ur => ur.Role)
                              .ThenInclude(r => r.RolePermissions)
                              .ThenInclude(rp => rp.Permission));

        if (user == null || !user.IsActive)
        {
            context.Fail();
            return;
        }

        var hasPermission = user.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions)
            .Any(rp => rp.Permission.Name == requirement.Permission);

        if (hasPermission)
            context.Succeed(requirement);
        else
            context.Fail();
    }
} 