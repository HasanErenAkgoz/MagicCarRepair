using AutoMapper;
using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MagicCarRepair.Application.Security.SecuredOperation;
using MediatR;
using IResult = Core.Packages.Application.Results.IResult;

namespace MagicCarRepair.Application.Features.Users.Commands.UpdateUser;

[SecuredOperation(Priority = 1)]
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == request.Id);
        if (user == null)
            return new ErrorResult("Kullanıcı bulunamadı");

        _mapper.Map(request, user);
        await _unitOfWork.SaveChangesAsync();
        
        return new SuccessResult("Kullanıcı başarıyla güncellendi");
    }
} 