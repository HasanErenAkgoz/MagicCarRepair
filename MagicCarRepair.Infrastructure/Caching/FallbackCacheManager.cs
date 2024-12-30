using MagicCarRepair.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace MagicCarRepair.Infrastructure.Caching;

public class FallbackCacheManager : ICacheManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;
    private readonly MemoryCacheManager _memoryCacheManager;

    public FallbackCacheManager(
        IMemoryCache memoryCache,
        IDistributedCache distributedCache,
        MemoryCacheManager memoryCacheManager)
    {
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _memoryCacheManager = memoryCacheManager;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        // Önce memory cache'e bak
        if (_memoryCache.TryGetValue(key, out T? value))
            return value;

        // Memory'de yoksa distributed cache'e bak
        var distributedValue = await _distributedCache.GetStringAsync(key);
        if (distributedValue == null)
            return default;

        // Distributed'dan alınan değeri memory'e de ekle
        var result = JsonSerializer.Deserialize<T>(distributedValue);
        if (result != null)
            await _memoryCacheManager.SetAsync(key, result);

        return result;
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        // Önce memory'den dene
        if (_memoryCache.TryGetValue(key, out T? value))
            return value!;

        // Sonra distributed'dan dene
        var distributedValue = await _distributedCache.GetStringAsync(key);
        if (distributedValue != null)
        {
            var result = JsonSerializer.Deserialize<T>(distributedValue);
            if (result != null)
            {
                await _memoryCacheManager.SetAsync(key, result, expiration);
                return result;
            }
        }

        // Her ikisinde de yoksa factory'den al ve cache'le
        value = await factory();

        await SetAsync(key, value, expiration);
        return value;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        // Her iki cache'e de kaydet
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _distributedCache.SetStringAsync(
            key,
            JsonSerializer.Serialize(value),
            options);

        await _memoryCacheManager.SetAsync(key, value, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
        _memoryCache.Remove(key);
    }

    public async Task RemoveByPrefixAsync(string prefixKey)
    {
        // Not: Distributed cache'de prefix ile silme işlemi için
        // özel bir mekanizma kurulması gerekebilir (örn: Redis için SCAN komutu)
        // Şimdilik sadece memory cache'den siliyoruz
        await _memoryCacheManager.RemoveByPrefixAsync(prefixKey);
    }
} 