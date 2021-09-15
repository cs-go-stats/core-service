using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup;
using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo.Setup
{
    internal class MongoSettingsConfiguration : StorageSettingsConfiguration<MongoDbConnectionSetting>
    {
        private MongoSettingsConfiguration(
            bool useStorage, 
            Action<MongoDbConnectionSettingBuilder> setup)
            : base(useStorage, BuildSetting, (Action<IStorageSettingsBuilder<MongoDbConnectionSetting>>) setup)
        {
        }

        public static MongoSettingsConfiguration Use() => new(true, _ => { });

        internal static MongoSettingsConfiguration UseWithModification(Action<MongoDbConnectionSettingBuilder> setup) => 
            new(true, setup);

        private static IStorageSettingsBuilder<MongoDbConnectionSetting> BuildSetting(IConfigurationRoot configuration) =>
            configuration.GetFromConfiguration(
                sectionName: "MongoConnection",
                creatingFunctor: configurationSection => new MongoDbConnectionSettingBuilder(
                    host: configurationSection["Host"],
                    port: configurationSection["Port"].Int(),
                    username: configurationSection["Username"],
                    password: configurationSection["Password"],
                    database: configurationSection["Database"]));
    }
}