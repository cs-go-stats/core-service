using System;
using System.Threading.Tasks;

namespace CSGOStats.Infrastructure.Core.Communication.Handling
{
    public interface IHandler
    {
    }

    public interface IMessageHandler : IHandler
    {
        Type HandlingType { get; }

        Task HandleAsync(object message);
    }

    public interface IMessageHandler<T> : IMessageHandler
    {
    }
}