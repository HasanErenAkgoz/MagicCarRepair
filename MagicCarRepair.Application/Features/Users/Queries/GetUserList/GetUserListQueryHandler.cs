using AutoMapper;
using Core.Packages.Application.Extensions;
using Core.Packages.Application.Repositories;
using Core.Packages.Application.Results;
using Core.Packages.Domain.Identity;
using MagicCarRepair.Application.Features.Users.Dtos;
using MagicCarRepair.Application.Security.SecuredOperation;
using MediatR;

namespace MagicCarRepair.Application.Features.Users.Queries.GetUserList;

[SecuredOperation(Priority = 1)]
public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IDataResult<IList<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<IList<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>()
            .GetListAsync(
                predicate: null,
                include: null
            );

        var pagedUsers = users
            .AsQueryable()
            .ApplyPaging(request.PageRequest)
            .ToList();

        var userDtos = _mapper.Map<IList<UserDto>>(pagedUsers);
        return new SuccessDataResult<IList<UserDto>>(userDtos, "Kullanıcılar başarıyla listelendi");
    }
} 