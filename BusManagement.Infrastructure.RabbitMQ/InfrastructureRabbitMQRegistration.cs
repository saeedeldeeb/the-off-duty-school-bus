using BusManagement.Core.Common.MessageBroker;
using BusManagement.Infrastructure.RabbitMQ.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace BusManagement.Infrastructure.RabbitMQ;

public static class InfrastructureRabbitMqRegistration
{
    public static IServiceCollection AddInfrastructureRabbitMq(
        this IServiceCollection services,
        string? uri
    )
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        services
            .AddSingleton<IRabbitMqProducer<TripStarted>, TripProducer>()
            .AddSingleton(_ => new ConnectionFactory { Uri = new Uri(uri) });
        return services;
    }
}
