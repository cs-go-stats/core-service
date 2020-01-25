using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.Mongo.Contexts
{
    public class TestMongoContext : BaseMongoContext
    {
        private static readonly string DatabaseName = $"Test-{DateTime.UtcNow.Ticks}";

        static TestMongoContext()
        {
            BsonMappings.Register();
        }

        public TestMongoContext()
            : base(new MongoDbConnectionSetting(
                host: "localhost",
                port: 27017,
                username: "root",
                password: "dotFive1",
                database: DatabaseName))
            {
            }

        public override void Dispose()
        {
            if (SessionHandle != null)
            {
                Client.DropDatabase(
                    session: SessionHandle,
                    name: DatabaseName);
            }

            base.Dispose();
        }
    }
}