using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.Communication.Config
{
    public class RetrySetting
    {
        public int RetryCount { get; }

        public RetrySetting(int retryCount)
        {
            RetryCount = retryCount.Positive(nameof(retryCount));
        }
    }
}