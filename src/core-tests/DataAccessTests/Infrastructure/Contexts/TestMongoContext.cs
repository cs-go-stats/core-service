using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Contexts
{
    public class TestMongoContext : BaseMongoContext
    {
        static TestMongoContext()
        {
            BsonMappings.Register();
        }

        public TestMongoContext(MongoDbConnectionSetting connectionSetting) 
            : base(connectionSetting)
        {
        }
    }
}