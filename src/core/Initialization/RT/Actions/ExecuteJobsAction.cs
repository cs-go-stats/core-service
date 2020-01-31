using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class ExecuteJobsAction : IRuntimeAction
    {
        public async Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            var scheduler = GetScheduler(serviceProvider);
            if (scheduler == null)
            {
                return;
            }

            var setup = serviceProvider.GetService<Func<IScheduler, IServiceProvider, IConfigurationRoot, Task>>();
            await setup(scheduler, serviceProvider, configuration);
            await scheduler.Start();
        }

        public Task StopAsync(IServiceProvider serviceProvider) => TryStopSchedulerAsync(serviceProvider);

        private static IScheduler GetScheduler(IServiceProvider serviceProvider) => serviceProvider.GetService<IScheduler>();

        private static Task TryStopSchedulerAsync(IServiceProvider serviceProvider) => GetScheduler(serviceProvider).Shutdown(false);
    }
}