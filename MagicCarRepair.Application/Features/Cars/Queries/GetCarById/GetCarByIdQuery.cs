using Core.Packages.Application.Results;
using MagicCarRepair.Application.Features.Cars.Dtos;
using MediatR;

namespace MagicCarRepair.Application.Features.Cars.Queries.GetCarById;

public class GetCarByIdQuery : IRequest<IDataResult<CarDto>>, IRequest<Core.Utilities.Results.IDataResult<CarDto>>
{
    public Guid Id { get; set; }
} 