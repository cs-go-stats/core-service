using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF
{
    public class PostgreConnectionSettings : IStorageSetting
    {
        public string Host { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }

        public bool IsAuditEnabled { get; }

        public PostgreConnectionSettings(string host, string database, string username, string password, bool isAuditEnabled)
        {
            Host = host;
            Database = database;
            Username = username;
            Password = password;
            IsAuditEnabled = isAuditEnabled;
        }

        public string GetConnectionString() =>
            $"Host={Host};Database={Database};Username={Username};Password={Password};";
    }
}