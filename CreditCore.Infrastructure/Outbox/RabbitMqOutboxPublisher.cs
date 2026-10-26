using CreditCore.Infrastructure.Persistence;
using RabbitMQ.Client;
using System.Text;

namespace CreditCore.Infrastructure.Outbox;

public sealed class RabbitMqOutboxPublisher : IOutboxPublisher, IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqOptions _options;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMqOutboxPublisher(RabbitMqOptions options)
    {
        _options = options;
        _connectionFactory = new ConnectionFactory
        {
            HostName = options.Host,
            Port = options.Port,
            UserName = options.UserName,
            Password = options.Password,
            AutomaticRecoveryEnabled = true
        };
    }

    public void Publish(OutboxMessage message)
    {
        var channel = GetOrCreateChannel();
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.MessageId = message.Id.ToString();
        properties.Type = message.Type;
        properties.Timestamp = new AmqpTimestamp(new DateTimeOffset(message.OccurredOnUtc).ToUnixTimeSeconds());

        try
        {
            channel.BasicPublish(
                exchange: _options.Exchange,
                routingKey: _options.RoutingKey,
                mandatory: true,
                basicProperties: properties,
                body: Encoding.UTF8.GetBytes(message.Payload));

            channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));
        }
        catch
        {
            ResetConnection();
            throw;
        }
    }

    public void Dispose() => ResetConnection();

    private IModel GetOrCreateChannel()
    {
        if (_channel?.IsOpen == true)
            return _channel;

        ResetConnection();
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(_options.Exchange, ExchangeType.Direct, durable: true);
        _channel.ExchangeDeclare(_options.DeadLetterExchange, ExchangeType.Direct, durable: true);

        _channel.QueueDeclare(
            queue: _options.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: new Dictionary<string, object>
            {
                ["x-dead-letter-exchange"] = _options.DeadLetterExchange,
                ["x-dead-letter-routing-key"] = _options.DeadLetterRoutingKey
            });
        _channel.QueueBind(_options.Queue, _options.Exchange, _options.RoutingKey);

        _channel.QueueDeclare(_options.DeadLetterQueue, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(_options.DeadLetterQueue, _options.DeadLetterExchange, _options.DeadLetterRoutingKey);

        _channel.ConfirmSelect();
        return _channel;
    }

    private void ResetConnection()
    {
        _channel?.Dispose();
        _channel = null;
        _connection?.Dispose();
        _connection = null;
    }
}
