using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public interface IRuntimeAction
    {
        Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration);

        Task StopAsync(IServiceProvider serviceProvider);
    }
}