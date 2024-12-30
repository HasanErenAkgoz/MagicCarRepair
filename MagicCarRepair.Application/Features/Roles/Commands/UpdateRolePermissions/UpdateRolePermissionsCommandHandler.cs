using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Security;
using Core.Utilities.Results;
using MagicCarRepair.Domain.Entities;
using MagicCarRepair.Application.Security.SecuredOperation;
using MediatR;
using IResult = Core.Packages.Application.Results.IResult;

namespace MagicCarRepair.Application.Features.Roles.Commands.UpdateRolePermissions;

[SecuredOperation(Priority = 1)]
public class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRolePermissionsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        var existingPermissions = await _unitOfWork.Repository<RolePermission>()
            .GetListAsync(rp => rp.RoleId == request.RoleId);

        foreach (var permission in existingPermissions)
        {
            await _unitOfWork.Repository<RolePermission>().DeleteAsync(permission);
        }

        foreach (var permissionId in request.PermissionIds)
        {
            await _unitOfWork.Repository<RolePermission>().AddAsync(new RolePermission
            {
                RoleId = request.RoleId,
                PermissionId = permissionId
            });
        }

        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult("Rol yetkileri güncellendi");
    }
} 