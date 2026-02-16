namespace CreditCore.Domain.Common
{
    public interface IUnitOfWork
    {
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
