using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Config;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    internal abstract class RabbitMqBasedEventBus : IEventBus, IMessageRegistrar
    {
        protected readonly RabbitMqConnectionConfiguration Configuration;
        protected readonly IServiceProvider ServiceProvider;

        protected RabbitMqBasedEventBus(RabbitMqConnectionConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration.NotNull(nameof(configuration));
            ServiceProvider = serviceProvider.NotNull(nameof(serviceProvider));
        }

        public abstract void Dispose();

        public abstract void RegisterForType(Type type);

        public abstract Task StartAsync();

        public abstract Task StopAsync();

        public Task PublishAsync(IMessage message) => PublishMessageAsync(message);

        public Task ScheduleAsync(Duration delay, IMessage message) => delay == Duration.Zero
            ? PublishMessageAsync(message)
            : RunScheduleCycleAsync(delay, message);

        protected async Task HandleMessageAsync(object rawMessage)
        {
            using var scope = ServiceProvider.CreateScope();
            var messageHandlers = FindCorrespondingHandlers(rawMessage, scope.ServiceProvider);
            if (!messageHandlers.Any())
            {
                return;
            }

            await CreatePipeline(scope.ServiceProvider).RunAsync(messageHandlers, rawMessage);
        }

        protected abstract Task PublishMessageAsync(IMessage message);

        protected static string GetQueueName(Type type) => type.Assembly.GetName().Name;

        private static IReadOnlyCollection<IMessageHandler> FindCorrespondingHandlers(object message, IServiceProvider serviceProvider) =>
            serviceProvider
                .GetServices<IHandler>()
                .Cast<IMessageHandler>()
                .Where(x => x.HandlingType == message.NotNull(nameof(message)).GetType())
                .ToArrayFast();

        private static IPipeline CreatePipeline(IServiceProvider serviceProvider)
        {
            serviceProvider.InitializeInScope();
            return new RetryingPipeline(
                pipes: serviceProvider.GetServices<IPipe>(),
                retrySetting: serviceProvider.GetService<RetrySetting>());
        }

        private async Task RunScheduleCycleAsync(Duration delay, IMessage message)
        {
            await Task.Delay(delay.ToTimeSpan());
            await PublishMessageAsync(message);
        }
    }
}