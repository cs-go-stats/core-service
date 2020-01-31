using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT
{
    public class Runtime
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfigurationRoot _configuration;

        public Runtime(
            ServiceProvider serviceProvider,
            IConfigurationRoot configuration)
        {
            _serviceProvider = serviceProvider.NotNull(nameof(serviceProvider));
            _configuration = configuration.NotNull(nameof(configuration));
        }

        public async Task RunAsync(IRuntimeAction action)
        {
            try
            {
                await action.NotNull(nameof(action)).ActAsync(_serviceProvider, _configuration);
            }
            finally
            {
                await action.StopAsync(_serviceProvider);
                await _serviceProvider.DisposeAsync();
            }
        }
    }
}