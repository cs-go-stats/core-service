using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CSGOStats.Infrastructure.Core.Scheduling
{
    public abstract class BaseJob : IJob
    {
        protected abstract string Code { get; }

        public async Task Execute(IJobExecutionContext context)
        {
            var serviceProvider = context.MergedJobDataMap.Get("ServiceProvider").OfType<IServiceProvider>();
            var logger = context.MergedJobDataMap.Get("Logger").OfType<ILogger>();
            using var scope = serviceProvider.CreateScope();

            try
            {
                logger.LogDebug($"Started executing job '{Code}'.");

                SetupContext(scope.ServiceProvider);
                await ExecuteAsync(scope.ServiceProvider);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Job '{Code}' haven't finished correctly because of error.");
            }
            finally
            {
                logger.LogDebug($"Finished job '{Code}' execution.");
            }
        }

        protected abstract Task ExecuteAsync(IServiceProvider serviceProvider);

        private void SetupContext(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.InitializeInScope();
            context.SetContext(Names.ScheduledJobs, Code);
        }
    }
}