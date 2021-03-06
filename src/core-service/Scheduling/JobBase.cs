﻿using System;
using System.Threading.Tasks;
using CSGOStats.Extensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CSGOStats.Services.Core.Scheduling
{
    public abstract class JobBase : IJob
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

                await ExecuteAsync(scope.ServiceProvider);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Job '{Code}' haven't finished correctly because of error.");
            }
            finally
            {
                logger.LogDebug($"Finished job '{Code}' execution.");
            }
        }

        protected abstract Task ExecuteAsync(IServiceProvider serviceProvider);
    }
}