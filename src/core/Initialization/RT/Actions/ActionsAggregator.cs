using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class ActionsAggregator : IRuntimeAction
    {
        private readonly IRuntimeAction[] _actions;

        public ActionsAggregator(params IRuntimeAction[] actions)
        {
            _actions = actions;
        }

        public async Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            foreach (var action in _actions)
            {
                await action.ActAsync(serviceProvider, configuration);
            }
        }

        public async Task StopAsync(IServiceProvider serviceProvider)
        {
            foreach (var action in _actions.Reverse())
            {
                await action.StopAsync(serviceProvider);
            }
        }
    }
}