using CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests.State
{
    public static class SharedData
    {
        public static TestMessage Message { get; set; }

        public static DelayedMessage DelayedMessage { get; set; }
    }
}