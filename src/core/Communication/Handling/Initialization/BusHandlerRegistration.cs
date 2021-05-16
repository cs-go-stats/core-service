using System;
using CSGOStats.Infrastructure.Core.Validation;
using MassTransit.RabbitMqTransport;

namespace CSGOStats.Infrastructure.Core.Communication.Handling.Initialization
{
    public class BusHandlerRegistration
    {
        public string MessageQueueName { get; }

        public Action<IRabbitMqReceiveEndpointConfigurator> ConfigureEndpoint { get; }

        public BusHandlerRegistration(string messageQueueName, Action<IRabbitMqReceiveEndpointConfigurator> configureEndpoint)
        {
            MessageQueueName = messageQueueName.NotNull(nameof(messageQueueName));
            ConfigureEndpoint = configureEndpoint.NotNull(nameof(configureEndpoint));
        }
    }
}