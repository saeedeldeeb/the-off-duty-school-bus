namespace BusManagement.Core.Common.MessageBroker;

public interface IRabbitMqProducer<in T>
{
    void Publish(T @event);
}
