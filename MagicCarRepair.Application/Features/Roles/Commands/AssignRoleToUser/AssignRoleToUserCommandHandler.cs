using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MediatR;

public class AssignRoleToUserCommand : IRequest<IResult>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleToUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var userRole = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };

        await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult("Rol kullanıcıya başarıyla atandı");
    }
} 