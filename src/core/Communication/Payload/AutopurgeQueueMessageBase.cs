using CSGOStats.Infrastructure.Core.Communication.Queues;

namespace CSGOStats.Infrastructure.Core.Communication.Payload
{
    [AutopurgeQueue]
    public abstract class AutopurgeQueueMessageBase : IMessage
    {
    }
}