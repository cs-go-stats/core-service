using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CSGOStats.Infrastructure.Core.Scheduling.Extensions
{
    public static class SchedulerExtensions
    {
        public static Task ScheduleJobAsync<TJob>(
            this IScheduler scheduler,
            IServiceProvider services,
            IConfigurationRoot configuration) where TJob : BaseJob =>
                scheduler.ScheduleJob(
                    services.CreateJobTemplate<TJob>(),
                    configuration.CreateCronScheduledTriggerFromConfiguration<TJob>());

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

        public static ITrigger CreateCronScheduledTriggerFromConfiguration(this IConfigurationRoot configuration, string configurationKey) => configuration
            .GetValue<string>(configurationKey)
            .CreateCronScheduledTrigger();

        public static ITrigger CreateCronScheduledTriggerFromConfiguration<TJob>(this IConfigurationRoot configuration) 
            where TJob : BaseJob => 
                configuration.CreateCronScheduledTriggerFromConfiguration(CreateJobConfigurationPath<TJob>());

        public static ITrigger CreateCronScheduledTrigger(this string cronExpression) => TriggerBuilder
            .Create()
            .WithSchedule(CronScheduleBuilder.CronSchedule(cronExpression))
            .Build();

        public static ITrigger CreateScheduledTrigger(this IScheduleBuilder scheduleBuilder) => TriggerBuilder
            .Create()
            .WithSchedule(scheduleBuilder)
            .Build();

        private static string CreateJobConfigurationPath<TJob>() where TJob : BaseJob => $"Jobs:{typeof(TJob).Name}:CronExpression";
    }
}