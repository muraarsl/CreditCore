using CreditCore.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace CreditCore.Infrastructure.Persistence
{
    public sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly CreditDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public EfUnitOfWork(CreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
    }
}
