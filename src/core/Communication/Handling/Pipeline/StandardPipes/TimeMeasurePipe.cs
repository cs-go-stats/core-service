using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Logging;

namespace CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline.StandardPipes
{
    // todo: usage of app insights
    public class TimeMeasurePipe : IPipe
    {
        private readonly ConcurrentDictionary<object, Stopwatch> _stopwatches = new ConcurrentDictionary<object, Stopwatch>();
        private readonly ILogger<LoggingPipe> _logger;

        public TimeMeasurePipe(ILogger<LoggingPipe> logger)
        {
            _logger = logger.NotNull(nameof(logger));
        }

        public Task OnStartAwait(object rawMessage)
        {
            _stopwatches.TryAdd(rawMessage, Stopwatch.StartNew());
            return Task.CompletedTask;
        }

        public Task OnExceptionAsync(object rawMessage, Exception _)
        {
            StopTimer(rawMessage);
            return Task.CompletedTask;
        }

        public Task OnCompleteAwait(object rawMessage)
        {
            var stopwatch = StopTimer(rawMessage);
            _logger.LogDebug($"Finished handling of message of type '{rawMessage.GetType()}' in '{stopwatch.Elapsed}'.");
            return Task.CompletedTask;
        }

        private Stopwatch StopTimer(object rawMessage)
        {
            _stopwatches.TryRemove(rawMessage, out var stopwatch);
            return stopwatch;
        }
    }
}