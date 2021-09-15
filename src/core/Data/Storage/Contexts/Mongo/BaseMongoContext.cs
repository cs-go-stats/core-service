using System.Threading.Tasks;
using MongoDB.Driver;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo
{
    public class BaseMongoContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public IClientSessionHandle SessionHandle { get; private set; }

        public BaseMongoContext(MongoDbConnectionSetting connectionSetting)
        {
            Client = new MongoClient(connectionSetting.CreateConnectionString());
            Database = Client.GetDatabase(
                name: connectionSetting.Database);
        }

        public virtual void Dispose() => SessionHandle?.Dispose();

        public async Task EnsureSessionCreatedAsync()
        {
            if (SessionHandle != null)
            {
                return;
            }

            SessionHandle = await Client.StartSessionAsync(new ClientSessionOptions { CausalConsistency = false });
        }
    }
}