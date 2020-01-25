using CSGOStats.Infrastructure.Core.Communication.Config;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline;
using CSGOStats.Infrastructure.Core.Communication.Handling.Pipeline.StandardPipes;
using CSGOStats.Infrastructure.Core.Communication.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class CommunicationServiceExtensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfigurationRoot configuration) =>
            services
                .RegisterPipeline()
                .RegisterBus(configuration)
                .AddSingleton(_ => configuration.GetFromConfiguration(
                    sectionName: "PipelineRetry",
                    configurationSection => new RetrySetting(
                        retryCount: configurationSection["RetryCount"].Int())));

        public static IServiceCollection AddHandlers<TAssembly>(this IServiceCollection services) =>
            services.Scan(selector =>
                selector.FromAssemblyOf<TAssembly>()
                    .AddClasses(classes => classes.AssignableTo(typeof(BaseMessageHandler<>)))
                    .As<IHandler>()
                    .WithTransientLifetime());

        private static IServiceCollection RegisterPipeline(this IServiceCollection services) =>
            services
                .AddSingleton<IPipe, LoggingPipe>()
                .AddSingleton<IPipe, TimeMeasurePipe>();

        private static IServiceCollection RegisterBus(this IServiceCollection services, IConfigurationRoot configuration) =>
            services
                .AddScoped<IEventBus, RabbitMqEventBus>()
                .AddScoped<IMessageRegistrar, RabbitMqEventBus>()
                .AddScoped(provider => new RabbitMqEventBus(
                    configuration: provider.GetService<RabbitMqConnectionConfiguration>(),
                    serviceProvider: provider))
                .ConfigureRabbitMqConnectionSetting(configuration);

        private static IServiceCollection ConfigureRabbitMqConnectionSetting(
            this IServiceCollection serviceProvider,
            IConfigurationRoot configuration) =>
                serviceProvider.AddSingleton(_ =>
                    configuration.GetFromConfiguration(
                        sectionName: "RabbitMqConnection",
                        creatingFunctor: configurationSection =>
                            new RabbitMqConnectionConfiguration(
                                host: configurationSection["Host"],
                                port: configurationSection["Port"].Int(),
                                username: configurationSection["Username"],
                                password: configurationSection["Password"],
                                heartbeat: configurationSection["Heartbeat"].Int())));
    }
}