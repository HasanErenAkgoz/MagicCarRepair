using MagicCarRepair.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Reflection;

namespace MagicCarRepair.Infrastructure.Caching;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly ConcurrentDictionary<string, bool> _allKeys;

    public MemoryCacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _allKeys = new ConcurrentDictionary<string, bool>();
    }

    public Task<T?> GetAsync<T>(string key)
    {
        return Task.FromResult(_memoryCache.TryGetValue(key, out T? value) ? value : default);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        if (_memoryCache.TryGetValue(key, out T? value))
            return value!;

        value = await factory();

        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
            options.AbsoluteExpirationRelativeToNow = expiration;

        options.RegisterPostEvictionCallback(RemoveKeyFromDictionary);

        _memoryCache.Set(key, value, options);
        _allKeys.TryAdd(key, true);

        return value;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
            options.AbsoluteExpirationRelativeToNow = expiration;

        options.RegisterPostEvictionCallback(RemoveKeyFromDictionary);

        _memoryCache.Set(key, value, options);
        _allKeys.TryAdd(key, true);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);
        _allKeys.TryRemove(key, out _);
        return Task.CompletedTask;
    }

    public Task RemoveByPrefixAsync(string prefixKey)
    {
        var keysToRemove = _allKeys.Keys.Where(k => k.StartsWith(prefixKey));

        foreach (var key in keysToRemove)
        {
            _memoryCache.Remove(key);
            _allKeys.TryRemove(key, out _);
        }

        return Task.CompletedTask;
    }

    private void RemoveKeyFromDictionary(object key, object value, EvictionReason reason, object state)
    {
        var stringKey = key.ToString();
        if (stringKey != null)
        {
            _allKeys.TryRemove(stringKey, out _);
        }
    }

    private static FieldInfo GetEntriesCollection(MemoryCache memoryCache)
    {
        return typeof(MemoryCache).GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance)!;
    }
} 