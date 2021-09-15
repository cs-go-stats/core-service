using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF.Setup;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo.Setup;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Initialization;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Contexts;
using CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model;
using CSGOStats.Infrastructure.Core.Tests.ExtensionsTests;
using CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSGOStats.Infrastructure.Core.Tests
{
    public class CoreTestFixture
    {
        private const string ServiceName = "CoreTest";

        public StartupBuilder StartupBuilder { get; }

        public CoreTestFixture()
        {
            StartupBuilder = Startup
                .ForEnvironment(ServiceName, Environments.Staging)
                .UsesPostgres<TestEfContext>(PostgresSettingsConfiguration.UseWithModification(x => x.ModifyDatabase(d => d.UseTestDatabaseName())))
                .UsesMongo<TestMongoContext>(MongoSettingsConfiguration.UseWithModification(x => x.ModifyDatabase(d => d.UseTestDatabaseName())))
                .ConfigureServices(ConfigureServices);
        }

        public Task RunAsync(IRuntimeAction action) => StartupBuilder.RunAsync(
            new ActionsAggregator(
                new InitializeRelationalDatabaseAction(),
                action,
                new DropRelationalDatabaseAction(),
                new DropMongoDatabaseAction()));

        private static IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot configuration) =>
            services
                .RegisterPostgresRepositoryFor<TestEntity>()
                .RegisterMongoRepositoryFor<TestDocument>(isGuidRepository: true)
                .AddScoped<IPageParser<TestPageModel>, PageParser<TestPageModel>>()
                .AddScoped<IValueMapperFactory, BaseDictionaryValueMapperFactory>()
                .AddScoped<IContentLoader>(_ => new FileContentLoader("PageParseTests/Pages/TestPage.html"));
    }
}