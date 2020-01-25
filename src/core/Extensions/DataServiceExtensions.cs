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
        public static IServiceCollection AddDataAccessConfiguration(this IServiceCollection services, IConfigurationRoot configuration, bool usesPostgres = true, bool usesMongo = true)
        {
            if (usesPostgres)
            {
                services.AddSingleton(_ =>
                    configuration.GetFromConfiguration(
                        sectionName: "PostgresConnection",
                        creatingFunctor: configurationSection => new PostgreConnectionSettings(
                            host: configurationSection["Host"],
                            database: configurationSection["Database"],
                            username: configurationSection["Username"],
                            password: configurationSection["Password"],
                            isAuditEnabled: configurationSection["IsAuditEnabled"].Bool())));
            }

            if (usesMongo)
            {
                services.AddSingleton(_ =>
                    configuration.GetFromConfiguration(
                        sectionName: "MongoConnection",
                        creatingFunctor: configurationSection => new MongoDbConnectionSetting(
                            host: configurationSection["Host"],
                            port: configurationSection["Port"].Int(),
                            username: configurationSection["Username"],
                            password: configurationSection["Password"],
                            database: configurationSection["Database"])));
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

        public static IServiceCollection RegisterPostgresContext<TContext>(this IServiceCollection services)
            where TContext : BaseDataContext =>
                services
                    .AddScoped<BaseDataContext, TContext>();

        public static IServiceCollection RegisterMongoContext<TContext>(this IServiceCollection services)
            where TContext : BaseMongoContext =>
                services
                    .AddScoped<BaseMongoContext, TContext>();
    }
}