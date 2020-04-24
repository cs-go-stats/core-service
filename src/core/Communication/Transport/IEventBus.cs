using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    public interface IEventBus : IDisposable
    {
        Task StartAsync();

        Task StopAsync();

        Task PublishAsync(IMessage message);

        Task ScheduleAsync(Duration delay, IMessage message);
    }
}