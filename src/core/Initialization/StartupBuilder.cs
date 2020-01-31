using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Initialization.RT;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.Initialization.Settings;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Serilog;

namespace CSGOStats.Infrastructure.Core.Initialization
{
    public class StartupBuilder
    {
        private readonly string _serviceName;
        private readonly IConfigurationRoot _configuration;
        private readonly ServiceCollection _serviceCollection;

        internal StartupBuilder(string serviceName, IConfigurationRoot configuration)
        {
            _serviceName = serviceName.NotNull(nameof(serviceName));
            _configuration = configuration.NotNull(nameof(configuration));
            _serviceCollection = new ServiceCollection();
            SetupServices();
        }

        public StartupBuilder WithMessaging<TStartup>()
        {
            _serviceCollection.AddMessaging(_configuration).AddHandlers<TStartup>();
            return this;
        }

        public StartupBuilder UsesPostgres<TContext>(Func<PostgreConnectionSettings, PostgreConnectionSettings> postgresConnectionSettingModifier = null)
            where TContext : BaseDataContext
        {
            _serviceCollection
                .AddDataAccessConfiguration(_configuration, usesPostgres: true, postgresConnectionSettingModifier: postgresConnectionSettingModifier)
                .RegisterPostgresContext<TContext>();
            return this;
        }

        public StartupBuilder UsesMongo<TContext>(Func<MongoDbConnectionSetting, MongoDbConnectionSetting> mongoConnectionSettingModifier = null)
            where TContext : BaseMongoContext
        {
            _serviceCollection
                .AddDataAccessConfiguration(_configuration, usesMongo: true, mongoConnectionSettingModifier: mongoConnectionSettingModifier)
                .RegisterMongoContext<TContext>();
            return this;
        }

        public async Task<StartupBuilder> WithJobsAsync(Func<IScheduler, IServiceProvider, IConfigurationRoot, Task> setup)
        {
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            _serviceCollection.AddSingleton(scheduler);
            _serviceCollection.AddSingleton(setup);

            return this;
        }

        public StartupBuilder ConfigureServices(Func<IServiceCollection, IConfigurationRoot, IServiceCollection> setup)
        {
            setup(_serviceCollection, _configuration);
            return this;
        }

        public Task RunAsync(IRuntimeAction action) =>
            new Runtime(_serviceCollection.BuildServiceProvider(), _configuration).RunAsync(action);

        private void SetupServices()
        {
            _serviceCollection.AddScoped<ExecutionContext>();
            EnableLogging();
        }

        private void EnableLogging() =>
            _serviceCollection.AddLogging(builder =>
            {
                var setting = _configuration.GetFromConfiguration(
                    sectionName: "Logging",
                    creatingFunctor: configurationSection => new LogSetting(
                        messageTemplate: configurationSection["MessageTemplate"],
                        minimumLevel: configurationSection["MinimumLevel"].Int()));
                builder
                    .AddSerilog(
                        new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .MinimumLevel.Is(setting.MinimumLevel)
                            .WriteTo.File(
                                path: $"{_serviceName}.log",
                                restrictedToMinimumLevel: setting.MinimumLevel,
                                outputTemplate: setting.MessageTemplate)
                            .WriteTo.Console(
                                restrictedToMinimumLevel: setting.MinimumLevel,
                                outputTemplate: setting.MessageTemplate)
                            .CreateLogger());
            });
    }
}