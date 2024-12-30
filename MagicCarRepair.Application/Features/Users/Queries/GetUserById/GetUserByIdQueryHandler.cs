using AutoMapper;
using Core.Packages.Application.Repositories;
using Core.Packages.Domain.Identity;
using Core.Utilities.Results;
using MagicCarRepair.Application.Features.Users.Dtos;
using MagicCarRepair.Application.Security.SecuredOperation;
using MagicCarRepair.Domain.Entities;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Queries.GetUserById;

[SecuredOperation(Priority = 1)]
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IDataResult<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == request.Id);
        if (user == null)
            return new ErrorDataResult<UserDto>("Kullanıcı bulunamadı");

        var userDto = _mapper.Map<UserDto>(user);
        return new SuccessDataResult<UserDto>(userDto);
    }
} 