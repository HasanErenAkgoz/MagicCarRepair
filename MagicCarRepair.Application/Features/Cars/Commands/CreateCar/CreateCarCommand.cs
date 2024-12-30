using Core.Packages.Application.Results;
using MediatR;

namespace MagicCarRepair.Application.Features.Cars.Commands.CreateCar;

public class CreateCarCommand : IRequest<IResult>, IRequest<Core.Utilities.Results.IResult>
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
    public Guid OwnerId { get; set; }
} 