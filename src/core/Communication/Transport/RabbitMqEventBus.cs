using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Queues;
using EasyNetQ;
using EasyNetQ.FluentConfiguration;
using EasyNetQ.NonGeneric;
using IBus = EasyNetQ.IBus;
using IMessage = CSGOStats.Infrastructure.Core.Communication.Payload.IMessage;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    internal class RabbitMqEventBus : RabbitMqBasedEventBus
    {
        private readonly IBus _bus;

        public RabbitMqEventBus(RabbitMqConnectionConfiguration configuration, IServiceProvider serviceProvider)
            : base(configuration, serviceProvider)
        {
            _bus = RabbitHutch.CreateBus(
                hostName: Configuration.Host,
                hostPort: (ushort) Configuration.Port,
                virtualHost: "/",
                username: Configuration.Username,
                password: Configuration.Password,
                requestedHeartbeat: (ushort) Configuration.Heartbeat,
                registerServices: _ => { });
        }

        public override void Dispose() => _bus.Dispose();

        public override void RegisterForType(Type type) =>
            _bus.SubscribeAsync(
                messageType: type,
                subscriptionId: Guid.NewGuid().ToString("D"),
                onMessage: HandleMessageAsync,
                configure: configuration => SubscriptionConfiguration(configuration, type));

        public override Task StartAsync() => Task.CompletedTask;

        public override Task StopAsync() => Task.CompletedTask;

        protected override Task PublishMessageAsync(IMessage message) => _bus.PublishAsync(message.GetType(), message);

        private void SubscriptionConfiguration(ISubscriptionConfiguration configuration, Type messageType)
        {
            var queueName = GetQueueName(messageType);
            var queue = _bus.Advanced.QueueDeclare(queueName);

            if (messageType.GetAttribute<AutopurgeQueueAttribute>() != null)
            {
                _bus.Advanced.QueuePurge(queue);
            }

            configuration.WithQueueName(queue.Name);
        }
    }
}