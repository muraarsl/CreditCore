namespace CreditCore.Infrastructure.Outbox;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMQ";

    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 5672;
    public string UserName { get; init; } = "guest";
    public string Password { get; init; } = "guest";
    public string Exchange { get; init; } = "credit.events";
    public string Queue { get; init; } = "credit.calculated";
    public string RoutingKey { get; init; } = "credit.calculated.v1";
    public string DeadLetterExchange { get; init; } = "credit.dlx";
    public string DeadLetterQueue { get; init; } = "credit.calculated.dlq";
    public string DeadLetterRoutingKey { get; init; } = "credit.calculated.dlq";
    
}
