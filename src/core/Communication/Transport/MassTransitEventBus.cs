using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling.Initialization;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Validation;
using MassTransit;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    internal class MassTransitEventBus : IEventBus
    {
        private readonly RabbitMqConnectionConfiguration _configuration;

        private IBusControl _busControl;

        public MassTransitEventBus(RabbitMqConnectionConfiguration configuration)
        {
            _configuration = configuration.NotNull(nameof(configuration));
        }

        public void Dispose() { }

        public Task StartAsync(BaseBusActivationHandler handler)
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host($"rabbitmq://{_configuration.Host}/", host =>
                {
                    host.Username(_configuration.Username);
                    host.Password(_configuration.Password);
                });

                foreach (var item in handler.HandlerRegistrations)
                {
                    sbc.ReceiveEndpoint(item.MessageQueueName, cfg => item.ConfigureEndpoint(cfg));
                }
            });

            return _busControl.StartAsync();
        }

        public Task StopAsync() => _busControl.StopAsync();

        public Task PublishAsync(IMessage message) => PublishMessageAsync(message);

        public Task ScheduleAsync(Duration delay, IMessage message) => delay == Duration.Zero
            ? PublishMessageAsync(message)
            : RunScheduleCycleAsync(delay, message);

        private Task PublishMessageAsync(IMessage message) => _busControl.Publish(message, x => x.Durable = true);

        private async Task RunScheduleCycleAsync(Duration delay, IMessage message)
        {
            await Task.Delay(delay.ToTimeSpan());
            await PublishMessageAsync(message);
        }
    }
}