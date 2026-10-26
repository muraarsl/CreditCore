using CreditCore.Infrastructure.Persistence;

namespace CreditCore.Infrastructure.Outbox;

public interface IOutboxPublisher
{
    void Publish(OutboxMessage message);
}
