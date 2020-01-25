using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline
{
    public interface IPipeline
    {
        Task RunAsync(IReadOnlyCollection<IMessageHandler> messageHandlers, object rawMessage);
    }
}