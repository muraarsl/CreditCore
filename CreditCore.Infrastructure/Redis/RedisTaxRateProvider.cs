using CreditCore.Domain.Credit;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System.Globalization;

public sealed class RedisTaxRateProvider : ITaxRateProvider
{
    private readonly StackExchange.Redis.IDatabase _db;

    public RedisTaxRateProvider(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public decimal GetBsmvRate()
        => GetRate("tax:rate:bsmv", 0.10m);

    public decimal GetKkdfRate()
        => GetRate("tax:rate:kkdf", 0.15m);

    private decimal GetRate(string key, decimal fallback)
    {
        var value = _db.StringGet(key);
        if (value.HasValue &&
            decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var rate))
        {
            return rate;
        }
        return fallback; // Redis yok / key yok / parse edilemedi
    }
}
