using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class CreateRelationalDatabaseAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration) =>
            serviceProvider.GetService<BaseDataContext>().Database.EnsureCreatedAsync();

        public Task StopAsync(IServiceProvider _) => Task.CompletedTask;
    }
}