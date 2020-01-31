using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class WaitForExternalInterruptionAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider _, IConfigurationRoot __) => Task.Delay(-1);

        public Task StopAsync(IServiceProvider _) => Task.CompletedTask;
    }
}