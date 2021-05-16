using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Config;
using CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Communication.Handling.Initialization
{
    public abstract class BaseBusActivationHandler
    {
        private readonly List<BusHandlerRegistration> _handlerRegistrations = new List<BusHandlerRegistration>();
        private readonly IServiceProvider _serviceProvider;

        public IReadOnlyCollection<BusHandlerRegistration> HandlerRegistrations => _handlerRegistrations;

        protected BaseBusActivationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public BaseBusActivationHandler RegisterHandler<TMessage>()
            where TMessage : class
        {
            _handlerRegistrations.Add(new BusHandlerRegistration(GetQueueName<TMessage>(), cfg => cfg.Handler<TMessage>(HandleMessageAsync)));
            return this;
        }

        private Task HandleMessageAsync<TMessage>(ConsumeContext<TMessage> context) 
            where TMessage : class => HandleAsync(context.Message);

        private async Task HandleAsync<TMessage>(TMessage rawMessage)
        {
            using var scope = _serviceProvider.CreateScope();
            var messageHandlers = FindCorrespondingHandlers(rawMessage, scope.ServiceProvider);
            if (!messageHandlers.Any())
            {
                return;
            }

            await CreatePipeline(scope.ServiceProvider).RunAsync(messageHandlers, rawMessage);
        }

        private static string GetQueueName<T>() => typeof(T).Assembly.GetName().Name;

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
    }
}