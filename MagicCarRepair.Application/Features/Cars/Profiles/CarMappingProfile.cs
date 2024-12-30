using AutoMapper;
using MagicCarRepair.Application.Features.Cars.Commands.CreateCar;
using MagicCarRepair.Application.Features.Cars.Dtos;
using MagicCarRepair.Domain.Entities;

namespace MagicCarRepair.Application.Features.Cars.Profiles;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.OwnerFullName, 
                opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.LastName}"));
        CreateMap<CreateCarCommand, Car>();
    }
} 