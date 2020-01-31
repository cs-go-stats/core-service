using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class FunctorAction : IRuntimeAction
    {
        private readonly Func<IServiceProvider, IConfigurationRoot, Task> _actAction;
        private readonly Func<IServiceProvider, Task> _stopAction;

        public FunctorAction(
            Func<IServiceProvider, IConfigurationRoot, Task> actAction, 
            Func<IServiceProvider, Task> stopAction = null)
        {
            _actAction = actAction.NotNull(nameof(actAction));
            _stopAction = stopAction;
        }

        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration) => _actAction(serviceProvider, configuration);

        public Task StopAsync(IServiceProvider serviceProvider) => _stopAction == null
            ? Task.CompletedTask
            : _stopAction(serviceProvider);
    }
}