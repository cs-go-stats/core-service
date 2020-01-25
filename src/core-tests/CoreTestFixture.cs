using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories.Mongo;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.EF.Contexts;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.EF.Repositories;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Model.EFModel;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Model.MongoModel;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Mongo.Contexts;

namespace CSGOStats.Infrastructure.Core.Tests
{
    public class CoreTestFixture : IDisposable
    {
        private readonly TestEfContext _efContext = new TestEfContext();
        private readonly TestMongoContext _mongoContext = new TestMongoContext();

        public IRepository<TestEntity> EntityRepository { get; }

        public IRepository<TestDocument> DocumentRepository { get; }

        public CoreTestFixture()
        {
            _efContext.Database.EnsureCreated();

            EntityRepository = new TestRepository(_efContext);
            DocumentRepository = new GuidKeyMongoRepository<TestDocument>(_mongoContext);
        }

        public void Dispose()
        {
            _efContext.Database.EnsureDeleted();

            _efContext.Dispose();
            _mongoContext.Dispose();
        }
    }
}