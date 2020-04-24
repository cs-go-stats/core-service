using System;
using CSGOStats.Infrastructure.Core.Communication.Payload;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages
{
    public class DelayedMessage : AutopurgeQueueMessageBase
    {
        public Guid Id { get; }

        public DelayedMessage(Guid id)
        {
            Id = id;
        }
    }
}