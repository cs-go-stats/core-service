using CSGOStats.Extensions.Extensions;
using CSGOStats.Extensions.Validation;
using Serilog.Events;

namespace CSGOStats.Services.Core.Initialization.Settings
{
    public class LogSetting
    {
        public string MessageTemplate { get; }

        public LogEventLevel MinimumLevel { get; }

        public LogSetting(string messageTemplate, int minimumLevel)
        {
            MessageTemplate = messageTemplate.NotNull(nameof(messageTemplate));
            MinimumLevel = minimumLevel.Natural(nameof(minimumLevel)).OfType<LogEventLevel>();
        }
    }
}