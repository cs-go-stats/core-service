using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Extensions;

namespace CSGOStats.Infrastructure.Core.Communication.Handling
{
    public abstract class BaseMessageHandler<T> : IMessageHandler<T>
        where T : class, IMessage
    {
        protected ExecutionContext Context { get; }

        public Type HandlingType => typeof(T);

        protected BaseMessageHandler(ExecutionContext context)
        {
            Context = context;
        }

        public abstract Task HandleAsync(T message);

        Task IMessageHandler.HandleAsync(object message) => HandleAsync(message.OfType<T>());
    }
}