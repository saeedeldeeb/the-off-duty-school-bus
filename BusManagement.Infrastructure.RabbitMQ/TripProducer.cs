using BusManagement.Infrastructure.RabbitMQ.Bases;
using BusManagement.Infrastructure.RabbitMQ.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BusManagement.Infrastructure.RabbitMQ;

public class TripProducer : ProducerBase<TripStarted>
{
    public TripProducer(
        ConnectionFactory connectionFactory,
        ILogger<RabbitMqClientBase> logger,
        ILogger<ProducerBase<TripStarted>> producerBaseLogger
    )
        : base(connectionFactory, logger, producerBaseLogger) { }

    protected override string ExchangeName => VirtualHost + ".TripExchange";
    protected override string RoutingKeyName => "trip.started";
    protected override string AppId => "TripProducer";
}
