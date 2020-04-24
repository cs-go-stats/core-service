using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages;
using CSGOStats.Infrastructure.Core.Tests.MessagingTests.State;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests.Handlers
{
    public class DelayedHandler : BaseMessageHandler<DelayedMessage>
    {
        public DelayedHandler(ExecutionContext context)
            : base(context)
        {
        }

        public override Task HandleAsync(DelayedMessage message)
        {
            SharedData.DelayedMessage = message;

            return Task.CompletedTask;
        }
    }
}