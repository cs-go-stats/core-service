using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Setup;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF
{
    public class PostgreConnectionSettingsBuilder : IStorageSettingsBuilder<PostgreConnectionSettings>
    {
        private string _host;
        private string _database;
        private string _username;
        private string _password;
        private bool _isAuditEnabled;

        internal PostgreConnectionSettingsBuilder(string host, string database, string username, string password, bool isAuditEnabled)
        {
            _host = host;
            _database = database;
            _username = username;
            _password = password;
            _isAuditEnabled = isAuditEnabled;
        }

        public PostgreConnectionSettingsBuilder ModifyHost(Func<string, string> host)
        {
            _host = host(_host);
            return this;
        }

        public PostgreConnectionSettingsBuilder ModifyDatabase(Func<string, string> database)
        {
            _database = database(_database);
            return this;
        }

        public PostgreConnectionSettingsBuilder ModifyUsername(Func<string, string> username)
        {
            _username = username(_username);
            return this;
        }

        public PostgreConnectionSettingsBuilder ModifyPassword(Func<string, string> password)
        {
            _password = password(_password);
            return this;
        }

        public PostgreConnectionSettingsBuilder ModifyAuditEnable(Func<bool, bool> auditEnable)
        {
            _isAuditEnabled = auditEnable(_isAuditEnabled);
            return this;
        }

        public PostgreConnectionSettings Construct() => new(
            host: _host,
            database: _database,
            username: _username,
            password: _password,
            isAuditEnabled: _isAuditEnabled);
    }
}