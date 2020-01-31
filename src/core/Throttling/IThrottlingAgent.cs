using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.Throttling
{
    public interface IThrottlingAgent
    {
        Task ThrottleAsync();
    }

    public class StaticThrottlingAgent : IThrottlingAgent
    {
        private readonly ThrottlingSetting _setting;

        public StaticThrottlingAgent(ThrottlingSetting setting)
        {
            _setting = setting.NotNull(nameof(setting));
        }

        public Task ThrottleAsync() => Task.Delay(_setting.Delay);
    }

    public class ThrottlingSetting
    {
        public TimeSpan Delay { get; }

        public ThrottlingSetting(TimeSpan delay)
        {
            Delay = delay;
        }
    }
}