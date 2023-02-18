using Icarus.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Icarus.Infrastructure.Services;

public sealed class CacheService : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
    private readonly IMemoryCache _memCache;

    public CacheService(IMemoryCache memCache)
    {
        _memCache = memCache;
    }

    public T? GetValue<T>(string key) where T : class
    {
        return _memCache.TryGetValue(key, out T value) ? value : null;
    }
    public void SetValue<T>(string key, object value, int cacheTimesInMinutes)
    {
        _memCache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTimesInMinutes)
        });

        CacheKeys.TryAdd(key, true);
    }
    public void RemoveValue(string key)
    {
        _memCache.Remove(key);

        CacheKeys.TryRemove(key, out _);
    }
    public void RemoveByPattern(string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            return;
        }

        foreach (string cacheKey in CacheKeys.Keys.Where(key => key.StartsWith(pattern)))
        {
            RemoveValue(cacheKey);
        }
    }
}
