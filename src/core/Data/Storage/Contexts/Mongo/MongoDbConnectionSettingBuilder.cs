using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo
{
    public class MongoDbConnectionSettingBuilder : IStorageSettingsBuilder<MongoDbConnectionSetting>
    {
        private string _host;
        private int _port;
        private string _username;
        private string _password;
        private string _database;

        internal MongoDbConnectionSettingBuilder(string host, int port, string username, string password, string database)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _database = database;
        }

        public MongoDbConnectionSetting Construct() => new(
            host: _host,
            port: _port,
            username: _username,
            password: _password,
            database: _database);

        public MongoDbConnectionSettingBuilder ModifyHost(Func<string, string> hostSetup)
        {
            _host = hostSetup(_host);
            return this;
        }

        public MongoDbConnectionSettingBuilder ModifyPort(Func<int, int> portSetup)
        {
            _port = portSetup(_port);
            return this;
        }

        public MongoDbConnectionSettingBuilder ModifyUsername(Func<string, string> usernameSetup)
        {
            _username = usernameSetup(_username);
            return this;
        }

        public MongoDbConnectionSettingBuilder ModifyPassword(Func<string, string> passwordSetup)
        {
            _password = passwordSetup(_password);
            return this;
        }

        public MongoDbConnectionSettingBuilder ModifyDatabase(Func<string, string> databaseSetup)
        {
            _database = databaseSetup(_database);
            return this;
        }
    }
}