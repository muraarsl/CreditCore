using CreditCore.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace CreditCore.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CreditDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(CreditDbContext context)
        {
            _context = context;
        }

        public async Task BeginAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
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
