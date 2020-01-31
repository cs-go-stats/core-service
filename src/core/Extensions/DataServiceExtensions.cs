using System;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories.EF;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class DataServiceExtensions
    {
        public static IServiceCollection AddDataAccessConfiguration(
            this IServiceCollection services,
            IConfigurationRoot configuration,
            bool usesPostgres = false,
            Func<PostgreConnectionSettings, PostgreConnectionSettings> postgresConnectionSettingModifier = null,
            bool usesMongo = false,
            Func<MongoDbConnectionSetting, MongoDbConnectionSetting> mongoConnectionSettingModifier = null)
        {
            if (usesPostgres)
            {
                var setting = configuration.GetFromConfiguration(
                    sectionName: "PostgresConnection",
                    creatingFunctor: configurationSection => new PostgreConnectionSettings(
                        host: configurationSection["Host"],
                        database: configurationSection["Database"],
                        username: configurationSection["Username"],
                        password: configurationSection["Password"],
                        isAuditEnabled: configurationSection["IsAuditEnabled"].Bool()));

                if (postgresConnectionSettingModifier != null)
                {
                    setting = postgresConnectionSettingModifier(setting);
                }

                services.AddSingleton(_ => setting);
            }

            if (usesMongo)
            {
                var setting = configuration.GetFromConfiguration(
                    sectionName: "MongoConnection",
                    creatingFunctor: configurationSection => new MongoDbConnectionSetting(
                        host: configurationSection["Host"],
                        port: configurationSection["Port"].Int(),
                        username: configurationSection["Username"],
                        password: configurationSection["Password"],
                        database: configurationSection["Database"]));

                if (mongoConnectionSettingModifier != null)
                {
                    setting = mongoConnectionSettingModifier(setting);
                }

                services.AddSingleton(_ => setting);
            }

            return services;
        }

        public static IServiceCollection RegisterPostgresRepositoryFor<TEntity>(this IServiceCollection services)
            where TEntity : class, IEntity =>
                services
                    .AddScoped<IRepository<TEntity>, EfRepository<TEntity>>();

        public static IServiceCollection RegisterMongoRepositoryFor<TEntity>(this IServiceCollection services, bool isGuidRepository = false)
            where TEntity : class, IEntity =>
                isGuidRepository
                    ? services.AddScoped<IRepository<TEntity>, GuidKeyMongoRepository<TEntity>>()
                    : services.AddScoped<IRepository<TEntity>, MongoRepository<TEntity>>();

        internal static IServiceCollection RegisterPostgresContext<TContext>(this IServiceCollection services)
            where TContext : BaseDataContext =>
                services
                    .AddScoped<BaseDataContext, TContext>();

        internal static IServiceCollection RegisterMongoContext<TContext>(this IServiceCollection services)
            where TContext : BaseMongoContext =>
                services
                    .AddScoped<BaseMongoContext, TContext>();
    }
}