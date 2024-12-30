namespace MagicCarRepair.Application.Features.Cars.Dtos;

public class CarDto
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerFullName { get; set; }
} 