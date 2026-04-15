using CreditCore.Domain.Credit;

namespace CreditCore.Application.Interfaces;

public interface ICreditRepositoryx
{
    Task AddAsync(CreditAggregate credit, CancellationToken cancellationToken = default);
    Task<CreditAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
