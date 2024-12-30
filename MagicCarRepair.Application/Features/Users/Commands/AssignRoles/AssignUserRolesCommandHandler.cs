using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepair.Application.Features.Users.Commands.AssignRoles;

public class AssignUserRolesCommandHandler : IRequestHandler<AssignUserRolesCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserRolesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(AssignUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Id == request.UserId,
                include: q => q.Include(u => u.UserRoles));

        if (user == null)
            return new ErrorResult("User not found");

        // Remove old roles
        user.UserRoles.Clear();

        // Add new roles
        foreach (var roleId in request.RoleIds)
        {
            user.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        await _unitOfWork.Repository<User>().UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult("User roles assigned successfully");
    }
} 