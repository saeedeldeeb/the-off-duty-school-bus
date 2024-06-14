using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BusManagement.Infrastructure.RabbitMQ.Bases;

public abstract class ConsumerBase : RabbitMqClientBase
{
    private readonly ILogger<ConsumerBase> _logger;
    protected abstract string QueueName { get; }

    public ConsumerBase(
        ConnectionFactory connectionFactory,
        ILogger<ConsumerBase> consumerLogger,
        ILogger<RabbitMqClientBase> logger
    )
        : base(connectionFactory, logger)
    {
        _logger = consumerLogger;
    }

    protected virtual T? OnEventReceived<T>(object sender, BasicDeliverEventArgs @event)
    {
        T? message = default;
        try
        {
            var body = Encoding.UTF8.GetString(@event.Body.ToArray());
            message = JsonConvert.DeserializeObject<T>(body);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error while retrieving message from queue.");
        }
        finally
        {
            Channel?.BasicAck(@event.DeliveryTag, false);
        }

        return message;
    }
}
