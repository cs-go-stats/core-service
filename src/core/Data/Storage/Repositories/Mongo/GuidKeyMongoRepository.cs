using System;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;
using MongoDB.Bson;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Repositories.Mongo
{
    public class GuidKeyMongoRepository<TEntity> : MongoRepository<TEntity>
        where TEntity : class, IEntity
    {
        public GuidKeyMongoRepository(BaseMongoContext context)
            : base(context)
        {
        }

        protected override BsonDocument CreateIdFilterDefinition<TKey>(TKey id)
        {
            if (id is Guid guid)
            {
                return new BsonDocument(
                    name: "Id",
                    value: new BsonBinaryData(guid));
            }

            return base.CreateIdFilterDefinition(id);
        }
    }
}