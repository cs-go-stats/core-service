using CSGOStats.Infrastructure.Core.Communication.Payload;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages
{
    public class TestMessage : AutopurgeQueueMessageBase
    {
        public string Id { get; }

        public long Version { get; }

        public TestData Data { get; }

        public TestMessage(string id, long version, TestData data)
        {
            Id = id;
            Version = version;
            Data = data;
        }
    }
}