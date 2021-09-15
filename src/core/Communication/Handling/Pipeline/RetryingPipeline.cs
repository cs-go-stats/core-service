using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Config;
using CSGOStats.Infrastructure.Core.Validation;
using Polly;

namespace CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline
{
    internal class RetryingPipeline : Pipeline
    {
        private readonly RetrySetting _retrySetting;

        public RetryingPipeline(IEnumerable<IPipe> pipes, RetrySetting retrySetting)
            : base(pipes)
        {
            _retrySetting = retrySetting.NotNull(nameof(retrySetting));
        }

        protected override Task HandleAsync(IMessageHandler messageHandler, object rawMessage) => Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(_retrySetting.RetryCount, _ => TimeSpan.FromSeconds(.1))
            .ExecuteAsync(() => TryHandleAsync(messageHandler, rawMessage));

        private static Task TryHandleAsync(IMessageHandler messageHandler, object rawMessage) =>
            messageHandler.HandleAsync(rawMessage);
    }
}