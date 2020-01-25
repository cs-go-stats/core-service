using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;
using CSGOStats.Infrastructure.Core.Validation;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Repositories.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity>
           where TEntity : class, IEntity
    {
        private readonly BaseMongoContext _context;

        public MongoRepository(BaseMongoContext context)
        {
            _context = context.NotNull(nameof(context));
        }

        public async Task<TEntity> FindAsync<TKey>(TKey id)
        {
            await _context.EnsureSessionCreatedAsync();

            var findResult = await GetCollection().FindAsync(
                session: _context.SessionHandle,
                filter: CreateIdFilterDefinition(id));
            return await findResult.MoveNextAsync().ContinueWith(x => x.Result ? findResult.Current.SingleOrDefault() : null);
        }

        public Task<TEntity> GetAsync<TKey>(TKey id) =>
            FindAsync(id).ContinueWith(x => x.Result ?? throw EntityNotFound.For<TEntity>(id));

        public async Task AddAsync<TKey>(TKey id, TEntity entity)
        {
            await _context.EnsureSessionCreatedAsync();
            await GetCollection().InsertOneAsync(
                session: _context.SessionHandle,
                document: entity);
        }

        public async Task UpdateAsync<TKey>(TKey id, TEntity entity)
        {
            await _context.EnsureSessionCreatedAsync();
            await GetCollection().ReplaceOneAsync(
                session: _context.SessionHandle,
                filter: CreateIdFilterDefinition(id),
                replacement: entity,
                options: new ReplaceOptions { IsUpsert = true });
        }

        public async Task DeleteAsync<TKey>(TKey id, TEntity entity)
        {
            await _context.EnsureSessionCreatedAsync();
            await GetCollection().DeleteOneAsync(
                session: _context.SessionHandle,
                filter: CreateIdFilterDefinition(id));
        }

        protected virtual BsonDocument CreateIdFilterDefinition<TKey>(TKey id) =>
            new BsonDocument(
                name: "Id",
                value: new BsonString(id.ToString()));

        private IMongoCollection<TEntity> GetCollection() =>
            _context.Database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}