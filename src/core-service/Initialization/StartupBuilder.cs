using System;
using System.Threading.Tasks;
using CSGOStats.Extensions.Extensions;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess;
using CSGOStats.Infrastructure.Messaging;
using CSGOStats.Services.Core.Initialization.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Serilog;

namespace CSGOStats.Services.Core.Initialization
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
            EnableLogging();
        }

        public StartupBuilder WithMessaging<TStartup>()
        {
            _serviceCollection.AddMessaging(_configuration).AddHandlers<TStartup>();
            return this;
        }

        public StartupBuilder UsesPostgres()
        {
            _serviceCollection.AddDataAccessConfiguration(_configuration, usesMongo: false);
            return this;
        }

        public StartupBuilder UsesMongo()
        {
            _serviceCollection.AddDataAccessConfiguration(_configuration, usesPostgres: false);
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

        public Task RunAsync(
            Func<IServiceProvider, Task> actionBeforeStart = null,
            Func<IServiceProvider, Task> actionBeforeFinish = null) =>
                new Runtime(
                    _serviceCollection.BuildServiceProvider(),
                    _configuration,
                    actionBeforeStart,
                    actionBeforeFinish).RunAsync();

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