using System;

namespace CSGOStats.Infrastructure.Core.Communication.Queues
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class AutopurgeQueueAttribute : Attribute
    {
    }
}