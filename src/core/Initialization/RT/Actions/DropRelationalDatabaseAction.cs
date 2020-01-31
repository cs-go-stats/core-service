using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class DropRelationalDatabaseAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider _, IConfigurationRoot __) => Task.CompletedTask;

        public Task StopAsync(IServiceProvider serviceProvider) => serviceProvider.GetService<BaseDataContext>().Database.EnsureDeletedAsync();
    }
}