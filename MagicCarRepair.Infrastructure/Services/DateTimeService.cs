using MagicCarRepair.Application.Interfaces;

namespace MagicCarRepair.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
} 