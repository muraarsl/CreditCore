using CreditCore.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class SqliteDatabaseStrategy : IDatabaseStrategy
{
    private readonly string _connectionString;

    public SqliteDatabaseStrategy(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("Sqlite")
            ?? "Data Source=CreditCore.db";
    }

    public bool IsAvailable()
    {
        return true;
    }

    public void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }
}