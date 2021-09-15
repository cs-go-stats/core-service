using System;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup
{
    public class StorageSettingsConfiguration<TSetting>
        where TSetting : class, IStorageSetting
    {
        private readonly Func<IConfigurationRoot, IStorageSettingsBuilder<TSetting>> _originalSettingProvider;
        private readonly Action<IStorageSettingsBuilder<TSetting>> _setup;
        
        public bool UseStorage { get; }

        protected StorageSettingsConfiguration(
            bool useStorage,
            Func<IConfigurationRoot, IStorageSettingsBuilder<TSetting>> originalSettingProvider,
            Action<IStorageSettingsBuilder<TSetting>> setup)
        {
            UseStorage = useStorage;
            _originalSettingProvider = originalSettingProvider;
            _setup = setup;
        }

        public TSetting Create(IConfigurationRoot configuration)
        {
            var settingBuilder = _originalSettingProvider(configuration);
            _setup(settingBuilder);
            
            return settingBuilder.Construct();
        }

        public static StorageSettingsConfiguration<TSetting> NoUse() => new(false, null, null);
        
        public static StorageSettingsConfiguration<TSetting> Use(Func<IConfigurationRoot, IStorageSettingsBuilder<TSetting>> originalProvider) =>
            new(true, originalProvider, _ => { });

        internal static StorageSettingsConfiguration<TSetting> UseWithModification(
            Func<IConfigurationRoot, IStorageSettingsBuilder<TSetting>> originalProvider,
            Action<IStorageSettingsBuilder<TSetting>> setup) => new(true, originalProvider, setup);
    }
}