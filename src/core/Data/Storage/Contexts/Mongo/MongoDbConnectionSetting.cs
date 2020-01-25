using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo
{
    public class MongoDbConnectionSetting
    {
        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }

        public string Database { get; }

        public MongoDbConnectionSetting(string host, int port, string username, string password, string database)
        {
            Host = host.NotNull(nameof(host));
            Port = port.Positive(nameof(port)).LessThanOrEqual(ushort.MaxValue, nameof(port));
            Username = username.NotNull(nameof(username));
            Password = password.NotNull(nameof(password));
            Database = database.NotNull(nameof(database));
        }

        public string CreateConnectionString() =>
            $"mongodb://{Username}:{Password}@{Host}:{Port}";
    }
}