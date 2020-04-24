using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class InitializeRelationalDatabaseAction : ActionsAggregator
    {
        public InitializeRelationalDatabaseAction()
            : base(
                new CreateRelationalDatabaseAction(),
                new MigrateRelationalDatabaseAction())
        {
        }
    }

    internal class CreateRelationalDatabaseAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration) =>
            serviceProvider.GetService<BaseDataContext>().Database.EnsureCreatedAsync();

        public Task StopAsync(IServiceProvider _) => Task.CompletedTask;
    }

    internal class MigrateRelationalDatabaseAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration) =>
            serviceProvider.GetService<BaseDataContext>().Database.MigrateAsync();

        public Task StopAsync(IServiceProvider _) => Task.CompletedTask;
    }
}