using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class DropMongoDatabaseAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider _, IConfigurationRoot __) => Task.CompletedTask;

        public Task StopAsync(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<BaseMongoContext>().Client.DropDatabaseAsync(
                serviceProvider.GetService<MongoDbConnectionSetting>().Database);
    }
}