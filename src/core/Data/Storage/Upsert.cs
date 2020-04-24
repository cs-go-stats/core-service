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

        public Task<TEntity> Async(TKey key, Action<TEntity> updater) => RunAsync(key, entity =>
        {
            updater(entity);
            return Task.CompletedTask;
        });

        public Task<TEntity> Async(TKey key, Func<TEntity, Task> updaterAsync) => RunAsync(key, updaterAsync);

        private async Task<TEntity> RunAsync(TKey key, Func<TEntity, Task> updaterAsync)
        {
            var entity = await _repository.FindAsync(key);
            if (entity == null)
            {
                entity = _factory.CreateEmpty(key);
                await updaterAsync(entity);

                await _repository.AddAsync(key, entity);

                return entity;
            }

            await updaterAsync(entity);

            await _repository.UpdateAsync(key, entity);

            return entity;
        }
    }
}