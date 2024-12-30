namespace MagicCarRepair.Application.Request;

public interface ITransactionalRequest
{
    // Core.Packages'daki UnitOfWork ile entegre çalışır
    // Bu interface'i implement eden command'lar otomatik olarak transaction içinde çalışır
}