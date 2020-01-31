using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Scheduling;
using CSGOStats.Infrastructure.Core.Tests.SchedulingTests.State;

namespace CSGOStats.Infrastructure.Core.Tests.SchedulingTests.Jobs
{
    public class TestJob : BaseJob
    {
        protected override string Code => nameof(TestJob);

        protected override Task ExecuteAsync(IServiceProvider _)
        {
            SharedState.Instance.Update();
            return Task.CompletedTask;
        }
    }
}