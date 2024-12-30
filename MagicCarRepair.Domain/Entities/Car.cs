using Core.Packages.Domain.Common;
using Core.Packages.Domain.Identity;

namespace MagicCarRepair.Domain.Entities;

public class Car : BaseEntity<Guid>
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
} 