using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup;
using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF.Setup
{
    public class PostgresSettingsConfiguration : StorageSettingsConfiguration<PostgreConnectionSettings>
    {
        private PostgresSettingsConfiguration(
            bool useStorage, 
            Action<PostgreConnectionSettingsBuilder> setup)
            : base(useStorage, BuildSetting, (Action<IStorageSettingsBuilder<PostgreConnectionSettings>>) setup)
        {
        }

        public static PostgresSettingsConfiguration Use() => new(true, _ => { });

        internal static PostgresSettingsConfiguration UseWithModification(Action<PostgreConnectionSettingsBuilder> setup) => 
            new(true, setup);

        private static IStorageSettingsBuilder<PostgreConnectionSettings> BuildSetting(IConfigurationRoot configuration) =>
            configuration.GetFromConfiguration(
                sectionName: "PostgresConnection",
                creatingFunctor: configurationSection => new PostgreConnectionSettingsBuilder(
                    host: configurationSection["Host"],
                    database: configurationSection["Database"],
                    username: configurationSection["Username"],
                    password: configurationSection["Password"],
                    isAuditEnabled: configurationSection["IsAuditEnabled"].Bool()));
    }
}