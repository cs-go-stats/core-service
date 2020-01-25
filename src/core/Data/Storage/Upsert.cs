using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.Data.Storage
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
            var entity = await _repository.FindAsync(key);
            if (entity == null)
            {
                entity = _factory.CreateEmpty(key);
                updater(entity);

                await _repository.AddAsync(key, entity);

                return entity;
            }

            updater(entity);

            await _repository.UpdateAsync(key, entity);

            return entity;
        }
    }
}