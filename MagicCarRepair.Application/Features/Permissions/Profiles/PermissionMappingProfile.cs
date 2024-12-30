using AutoMapper;
using MagicCarRepair.Application.Features.Permissions.Dtos;
using MagicCarRepair.Domain.Entities;

namespace MagicCarRepair.Application.Features.Permissions.Profiles;

public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionDto>();
        CreateMap<PermissionDto, Permission>();
    }
} 