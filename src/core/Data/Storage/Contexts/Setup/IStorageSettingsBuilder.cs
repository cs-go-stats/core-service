namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup
{
    public interface IStorageSettingsBuilder<out TSetting>
        where TSetting : class, IStorageSetting
    {
        TSetting Construct();
    }
}