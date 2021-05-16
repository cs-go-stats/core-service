using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling.Initialization;
using CSGOStats.Infrastructure.Core.Communication.Transport;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class InitializeMessagingAction : ActionsAggregator
    {
        public InitializeMessagingAction(Action<BaseBusActivationHandler> registrationAction)
            : base(
                new RegisterMessagesHandlersAction(registrationAction), 
                new ListenToEventsAction())
        {
        }
    }

    public class RegisterMessagesHandlersAction : IRuntimeAction
    {
        private readonly Action<BaseBusActivationHandler> _registrationAction;

        public RegisterMessagesHandlersAction(Action<BaseBusActivationHandler> registrationAction)
        {
            _registrationAction = registrationAction.NotNull(nameof(registrationAction));
        }

        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            _registrationAction(serviceProvider.GetService<BaseBusActivationHandler>());
            return Task.CompletedTask;
        }

        public Task StopAsync(IServiceProvider _) => Task.CompletedTask;
    }

    public class ListenToEventsAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration) =>
            serviceProvider.GetService<IEventBus>().StartAsync(serviceProvider.GetService<BaseBusActivationHandler>());

        public Task StopAsync(IServiceProvider serviceProvider) => serviceProvider.GetService<IEventBus>().StopAsync();
    }
}