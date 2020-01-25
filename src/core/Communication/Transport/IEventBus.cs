using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Payload;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    public interface IEventBus : IDisposable
    {
        Task PublishAsync(IMessage message);
    }
}