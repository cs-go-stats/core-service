﻿using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.Tests.SchedulingTests.Jobs;
using CSGOStats.Infrastructure.Core.Tests.SchedulingTests.State;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Quartz;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.SchedulingTests
{
    public class SchedulerTests : FixtureBasedTest
    {
        public SchedulerTests(CoreTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task SchedulerSimpleTest()
        {
            await Fixture.StartupBuilder.WithJobsAsync((scheduler, services, _) => scheduler.ScheduleJob(
                services.CreateJobTemplate<TestJob>(),
                SimpleScheduleBuilder.RepeatSecondlyForTotalCount(1).CreateScheduledTrigger()));

            var initialCounterState = SharedState.Instance.Counter;

            await Fixture.RunAsync(new ActionsAggregator(
                new ExecuteJobsAction(),
                new FunctorAction((_, __) => SchedulerTestAction(_, __, initialCounterState))));
        }

        private static async Task SchedulerTestAction(IServiceProvider _, IConfigurationRoot __, int initialCounterValue)
        {
            await Task.Delay(TimeSpan.FromSeconds(1.1));
            SharedState.Instance.Counter.Should().BeGreaterThan(initialCounterValue);
            SharedState.Instance.Counter.Should().Be(initialCounterValue + 1);
        }
    }
}