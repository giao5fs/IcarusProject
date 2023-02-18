namespace Icarus.Application.Abstractions.Caching;

public interface ICacheService
{
    T? GetValue<T>(string key) where T : class;
    void SetValue<T>(string key, object value, int cacheTimesInMinutes);
    void RemoveValue(string key);
    void RemoveByPattern(string pattern);
}
