using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CSGOStats.Infrastructure.Core.Initialization
{
    internal class Runtime
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfigurationRoot _configuration;
        private readonly Func<IServiceProvider, Task> _actionBeforeStart;
        private readonly Func<IServiceProvider, Task> _actionBeforeFinish;

        public Runtime(
            ServiceProvider serviceProvider,
            IConfigurationRoot configuration,
            Func<IServiceProvider, Task> actionBeforeStart,
            Func<IServiceProvider, Task> actionBeforeFinish)
        {
            _serviceProvider = serviceProvider.NotNull(nameof(serviceProvider));
            _configuration = configuration.NotNull(nameof(configuration));
            _actionBeforeStart = actionBeforeStart;
            _actionBeforeFinish = actionBeforeFinish;
        }

        public async Task RunAsync()
        {
            try
            {
                if (_actionBeforeStart != null)
                {
                    await _actionBeforeStart(_serviceProvider);
                }

                await TryStartSchedulerAsync();
                await Task.Delay(-1);
            }
            finally
            {
                if (_actionBeforeFinish != null)
                {
                    await _actionBeforeFinish(_serviceProvider);
                }

                await TryStopSchedulerAsync();
                await _serviceProvider.DisposeAsync();
            }
        }

        private async Task TryStartSchedulerAsync()
        {
            var scheduler= GetScheduler();
            if (scheduler == null)
            {
                return;
            }

            var setup = _serviceProvider.GetService<Func<IScheduler, IServiceProvider, IConfigurationRoot, Task>>();
            await setup(scheduler, _serviceProvider, _configuration);
            await scheduler.Start();
        }

        private Task TryStopSchedulerAsync() =>
            GetScheduler()?.Shutdown(false) ?? Task.CompletedTask;

        private IScheduler GetScheduler() =>
            _serviceProvider.GetService<IScheduler>();
    }
}