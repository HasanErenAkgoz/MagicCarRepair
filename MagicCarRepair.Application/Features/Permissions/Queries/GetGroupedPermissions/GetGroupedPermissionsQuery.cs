using AutoMapper;
using Core.Packages.Application.Repositories;
using Core.Packages.Application.Results;
using MagicCarRepair.Application.Features.Permissions.Constants;
using MagicCarRepair.Application.Features.Permissions.Dtos;
using MagicCarRepair.Domain.Entities;
using MediatR;

namespace MagicCarRepair.Application.Features.Permissions.Queries.GetGroupedPermissions;

public class GetGroupedPermissionsQuery : IRequest<IDataResult<Dictionary<string, List<PermissionDto>>>>
{
    public string[] Roles => new[] { PermissionOperationClaims.View };
}

public class GetGroupedPermissionsQueryHandler : IRequestHandler<GetGroupedPermissionsQuery, IDataResult<Dictionary<string, List<PermissionDto>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGroupedPermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<Dictionary<string, List<PermissionDto>>>> Handle(
        GetGroupedPermissionsQuery request, 
        CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork.Repository<Permission>()
            .GetListAsync();

        var groupedPermissions = permissions
            .GroupBy(p => p.Group)
            .ToDictionary(
                g => g.Key,
                g => _mapper.Map<List<PermissionDto>>(g.ToList())
            );

        return new SuccessDataResult<Dictionary<string, List<PermissionDto>>>(
            groupedPermissions, 
            "Yetkiler başarıyla listelendi"
        );
    }
} 