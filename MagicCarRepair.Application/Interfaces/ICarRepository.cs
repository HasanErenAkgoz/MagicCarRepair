using Core.Packages.Application.Repositories;
using MagicCarRepair.Domain.Entities;

namespace MagicCarRepair.Application.Interfaces;

public interface ICarRepository : IAsyncRepository<Car>
{
    // Car'a özel metodlar buraya eklenebilir
} 