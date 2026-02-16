using CreditCore.Application.Interfaces;
using CreditCore.Domain.Credit;
using CreditCore.Infrastructure.Persistence;
using CreditCore.Infrastructure.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CreditCore.Infrastructure.Repositories;

public class CreditRepository : ICreditRepository
{
    private readonly CreditDbContext _dbContext;

    public CreditRepository(CreditDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        CreditAggregate credit,
        CancellationToken cancellationToken = default)
    {
        var entity = credit.ToEntity();

        await _dbContext.Credits.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<CreditAggregate?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Credits
            .Include(x => x.Installments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        // Şu an ToDomain yok → ileride eklenebilir
        throw new NotSupportedException(
            "Loading CreditAggregate from persistence is not supported yet.");
    }
}
