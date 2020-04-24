using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    internal class MassTransitEventBus : RabbitMqBasedEventBus
    {
        private IRabbitMqBusFactoryConfigurator _busFactoryConfigurator;
        private readonly IBusControl _busControl;

        public MassTransitEventBus(RabbitMqConnectionConfiguration configuration, IServiceProvider serviceProvider)
            : base(configuration, serviceProvider)
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host($"rabbitmq://{Configuration.Host}/", host =>
                {
                    host.Username(Configuration.Username);
                    host.Password(Configuration.Password);
                });

                _busFactoryConfigurator = sbc;
            });
        }

        public override void Dispose() { }

        public override void RegisterForType(Type type) => _busFactoryConfigurator.ReceiveEndpoint(
            GetQueueName(type),
            cfg => cfg.Handler<IMessage>(HandleAsync));

        public override Task StartAsync() => _busControl.StartAsync();

        public override Task StopAsync() => _busControl.StopAsync();

        protected override Task PublishMessageAsync(IMessage message) => _busControl.Publish(message, x => x.Durable = true);

        private Task HandleAsync<T>(ConsumeContext<T> context) where T : class => HandleMessageAsync(context.Message);
    }
}