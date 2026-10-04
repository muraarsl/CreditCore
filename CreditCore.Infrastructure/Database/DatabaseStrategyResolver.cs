using CreditCore.Infrastructure.Database;

public class DatabaseStrategyResolver
{
    private readonly IEnumerable<IDatabaseStrategy> _strategies;

    public DatabaseStrategyResolver(
        IEnumerable<IDatabaseStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IDatabaseStrategy Resolve()
    {
        return _strategies.First(x => x.IsAvailable());
    }
}