using AutoMapper;
using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MagicCarRepair.Application.Security.SecuredOperation;
using MediatR;
using IResult = Core.Packages.Application.Results.IResult;

namespace MagicCarRepair.Application.Features.Users.Commands.CreateUser;

[SecuredOperation(Priority = 1)]
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        return new SuccessResult("Kullanıcı başarıyla oluşturuldu");
    }
} 