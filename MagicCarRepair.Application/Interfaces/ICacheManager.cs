namespace MagicCarRepair.Application.Interfaces;

public interface ICacheManager
{
    Task<T?> GetAsync<T>(string key);
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task RemoveByPrefixAsync(string prefixKey);
} 