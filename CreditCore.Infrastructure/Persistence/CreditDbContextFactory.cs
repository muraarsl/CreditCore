using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CreditCore.Infrastructure.Persistence;

public class CreditDbContextFactory
    : IDesignTimeDbContextFactory<CreditDbContext>
{
    public CreditDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CreditDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=.;Database=CreditCoreDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new CreditDbContext(optionsBuilder.Options);
    }
}
