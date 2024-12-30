using Core.Packages.Infrastructure.Repositories;
using MagicCarRepair.Application.Interfaces;
using MagicCarRepair.Domain.Entities;
using MagicCarRepair.Infrastructure.Persistence;

public class CarRepository : EfRepositoryBase<Car, MagicCarRepairDbContext>, ICarRepository
{
    public CarRepository(MagicCarRepairDbContext context) : base(context)
    {
    }
    
    // Sadece Car entity'sine özel metodlar buraya eklenebilir
} 