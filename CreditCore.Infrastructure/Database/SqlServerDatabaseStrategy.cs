using CreditCore.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class SqlServerDatabaseStrategy : IDatabaseStrategy
{
    private readonly string _connectionString;

    public SqlServerDatabaseStrategy(IConfiguration configuration)
    {
        _connectionString =
                       //configuration.GetConnectionString("CreditDb")
                       "Server=localhost;Database=CreditCoreDb;Trusted_Connection=True;TrustServerCertificate=True;";

            //?? throw new InvalidOperationException(
            //    "CreditDb connection string bulunamadı.");
    }

    public bool IsAvailable()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}