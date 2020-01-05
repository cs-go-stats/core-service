using System;
using System.Threading.Tasks;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Entities;
using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Services.Core.Handling.Entities;

namespace CSGOStats.Services.Core.Handling.Storage
{
    public class Upsert<TEntity, TKey>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IEntityFactory<TEntity, TKey> _factory;

        public Upsert(IRepository<TEntity> repository, IEntityFactory<TEntity, TKey> factory)
        {
            _repository = repository.NotNull(nameof(repository));
            _factory = factory.NotNull(nameof(factory));
        }

        public virtual async Task<TEntity> Async(TKey key, Action<TEntity> updater)
        {
            var entity = await _repository.FindAsync(key) ?? _factory.CreateEmpty(key);

            updater(entity);

            await _repository.UpdateAsync(key, entity);

            return entity;
        }
    }
}