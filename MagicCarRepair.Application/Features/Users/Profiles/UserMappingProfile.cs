using AutoMapper;
using Core.Packages.Domain.Identity;
using MagicCarRepair.Application.Features.Users.Commands.CreateUser;
using MagicCarRepair.Application.Features.Users.Commands.UpdateUser;
using MagicCarRepair.Application.Features.Users.Dtos;
using MagicCarRepair.Domain.Entities;

namespace MagicCarRepair.Application.Features.Users.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
} 