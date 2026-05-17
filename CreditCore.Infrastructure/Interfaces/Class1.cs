using CreditCore.Domain.Credit;

namespace CreditCore.Infrastructure.Interfaces;

public interface ICreditRepository
{
    Task AddAsync(CreditAggregate credit, CancellationToken cancellationToken = default);
    Task<CreditAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}