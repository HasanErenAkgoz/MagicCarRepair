using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Packages.Domain.Security;
using Core.Utilities.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepair.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Repository<Role>()
            .GetAsync(r => r.Id == request.Id, 
                include: q => q.Include(r => r.RolePermissions));

        if (role == null)
            return new ErrorResult("Role not found");

        role.Name = request.Name;
        role.Description = request.Description;

        // Remove old permissions
        role.RolePermissions.Clear();

        // Add new permissions
        foreach (var permissionId in request.PermissionIds)
        {
            role.RolePermissions.Add(new RolePermission
            {
                PermissionId = permissionId,
                RoleId = role.Id
            });
        }

        await _unitOfWork.Repository<Role>().UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult("Role updated successfully");
    }
} 