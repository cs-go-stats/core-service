using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Extensions;

namespace CSGOStats.Infrastructure.Core.Communication.Handling
{
    public abstract class BaseMessageHandler<T> : IMessageHandler
        where T : class, IMessage
    {
        public Type HandlingType => typeof(T);

        public abstract Task HandleAsync(T message);

        Task IMessageHandler.HandleAsync(object message) => HandleAsync(message.OfType<T>());
    }
}