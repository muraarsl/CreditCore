
using System.Text;
using System.Text.Json;
using CreditCore.Infrastructure.Outbox;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CreditCore.Contracts;


namespace CreditCore.Infrastructure.Messaging;

public sealed class RabbitMqConsumer : BackgroundService
{
    private readonly RabbitMqOptions _options;
    private readonly ILogger<RabbitMqConsumer> _logger;

    private IConnection? _connection;
    private IModel? _channel;
    private readonly object _channelLock = new();

    public RabbitMqConsumer(
        RabbitMqOptions options,
        ILogger<RabbitMqConsumer> logger)
    {
        _options = options;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password,
            AutomaticRecoveryEnabled = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(
            _options.Exchange,
            ExchangeType.Direct,
            durable: true);

        _channel.ExchangeDeclare(
            _options.DeadLetterExchange,
            ExchangeType.Direct,
            durable: true);

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

        _channel.QueueBind(
            _options.Queue,
            _options.Exchange,
            _options.RoutingKey);

        _channel.QueueDeclare(
            queue: _options.DeadLetterQueue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        _channel.QueueBind(
            _options.DeadLetterQueue,
            _options.DeadLetterExchange,
            _options.DeadLetterRoutingKey);

        _channel.BasicQos(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (_, args) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(
                    args.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<CreditCalculatedEvent>(
                        json);

                throw new InvalidOperationException("DLQ test hatası");

                if (message is null)
                {
                    throw new JsonException(
                        "CreditCalculatedEvent mesajı boş.");
                }

                _logger.LogInformation(
                    "CreditCalculatedEvent received. " +
                    "CreditId: {CreditId}, TotalPayment: {TotalPayment}",
                    message.CreditId,
                    message.TotalPayment);

                lock (_channelLock)
                {
                    _channel?.BasicAck(
                        args.DeliveryTag,
                        multiple: false);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "RabbitMQ mesajı işlenemedi.");

                try
                {
                    lock (_channelLock)
                    {
                        _channel?.BasicNack(
                            args.DeliveryTag,
                            multiple: false,
                            requeue: false);
                    }
                }
                catch (Exception nackException)
                {
                    _logger.LogError(
                        nackException,
                        "RabbitMQ mesajı reddedilemedi.");
                }
            }
        };

        _channel.BasicConsume(
            queue: _options.Queue,
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation(
            "RabbitMQ Consumer started. Queue: {Queue}",
            _options.Queue);

        try
        {
            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
        catch (OperationCanceledException)
            when (stoppingToken.IsCancellationRequested)
        {
            // Uygulama kontrollü olarak kapanıyor.
        }
    }

    public override void Dispose()
    {
        lock (_channelLock)
        {
            _channel?.Dispose();
            _channel = null;

            _connection?.Dispose();
            _connection = null;
        }

        base.Dispose();
    }
}
