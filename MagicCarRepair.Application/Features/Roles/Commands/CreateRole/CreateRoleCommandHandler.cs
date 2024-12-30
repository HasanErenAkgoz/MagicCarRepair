using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Packages.Domain.Security;
using Core.Utilities.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = request.Name,
            Description = request.Description
        };

        foreach (var permissionId in request.PermissionIds)
        {
            role.RolePermissions.Add(new RolePermission
            {
                PermissionId = permissionId,
                Role = role
            });
        }

        await _unitOfWork.Repository<Role>().AddAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult("Role created successfully");
    }
} 