using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup
{
    internal static class StorageSettingRegistrationExtensions
    {
        public static IServiceCollection TryRegisterDataConfiguration<TSetting>(
            this IServiceCollection services,
            StorageSettingsConfiguration<TSetting> storageSettingsConfiguration,
            IConfigurationRoot configuration) where TSetting : class, IStorageSetting
        {
            if (!storageSettingsConfiguration.UseStorage)
            {
                return services;
            }

            var setting = storageSettingsConfiguration.Create(configuration);
            return services.AddSingleton(_ => setting);
        }
    }
}