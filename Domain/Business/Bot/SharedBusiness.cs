using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Caching.Memory;

namespace Domain.Business.Bot;

public class SharedBusiness : ISharedBusiness
{
    private static readonly MemoryCache _dailyUserCache
        = new MemoryCache(new MemoryCacheOptions());

    public async Task<bool> DailyCache(DateTime date)
    {
        if (_dailyUserCache.TryGetValue(date, out bool cached))
        {
            return cached;
        }
        var expiration = date.Date.AddDays(1) - DateTime.UtcNow;
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration > TimeSpan.Zero ? expiration : TimeSpan.FromHours(24)
        };

        _dailyUserCache.Set(date, true, options);
        return false;
    }

    public async Task<bool> MounthJokeCache(string user, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(user))
            return false;

        if (_dailyUserCache.TryGetValue(user, out DateTime lastCalled))
        {
            if (lastCalled.Date == date.Date)
                return true;
        }
        var expiration = date.Date.AddDays(1) - DateTime.UtcNow;
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration > TimeSpan.Zero ? expiration : TimeSpan.FromHours(24)
        };

        _dailyUserCache.Set(user, date, options);
        return false;
    }
}
