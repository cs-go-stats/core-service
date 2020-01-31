using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class SchedulerExtensions
    {
        public static IJobDetail CreateJobTemplate<TJob>(
            this IServiceProvider serviceProvider, 
            Action<IDictionary<string, object>> dataExtension = null)
                where TJob : IJob
        {
            IDictionary<string, object> dataMap = new Dictionary<string, object>
            {
                ["ServiceProvider"] = serviceProvider,
                ["Logger"] = serviceProvider.GetService<ILogger<TJob>>()
            };
            dataExtension?.Invoke(dataMap);

            return JobBuilder
                .Create<TJob>()
                .SetJobData(new JobDataMap(dataMap))
                .StoreDurably()
                .Build();
        }

        public static ITrigger CreateCronScheduledTriggerFromConfiguration(
            IConfigurationRoot configuration,
            string configurationKey) =>
            configuration
                .GetValue<string>(configurationKey)
                .CreateCronScheduledTrigger();

        public static ITrigger CreateCronScheduledTrigger(this string cronExpression) =>
            TriggerBuilder
                .Create()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExpression))
                .Build();

        public static ITrigger CreateScheduledTrigger(this IScheduleBuilder scheduleBuilder) =>
            TriggerBuilder
                .Create()
                .WithSchedule(scheduleBuilder)
                .Build();
    }
}