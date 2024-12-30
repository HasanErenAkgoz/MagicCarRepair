using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MagicCarRepair.Application.Security.SecuredOperation;
using MediatR;
using IResult = Core.Packages.Application.Results.IResult;

namespace MagicCarRepair.Application.Features.Users.Commands.DeleteUser;

[SecuredOperation(Priority = 1)]
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == request.Id);
        if (user == null)
            return new ErrorResult("Kullanıcı bulunamadı");

        await _unitOfWork.Repository<User>().DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        return new SuccessResult("Kullanıcı başarıyla silindi");
    }
} 