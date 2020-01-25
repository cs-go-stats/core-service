using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo
{
    public class BaseMongoContext
    {
        protected readonly IMongoClient Client;

        public IMongoDatabase Database { get; }

        public IClientSessionHandle SessionHandle { get; private set; }

        static BaseMongoContext()
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        }

        public BaseMongoContext(MongoDbConnectionSetting connectionSetting)
        {
            Client = new MongoClient(connectionSetting.CreateConnectionString());
            Database = Client.GetDatabase(
                name: connectionSetting.Database,
                settings: new MongoDatabaseSettings { GuidRepresentation = GuidRepresentation.Standard });
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