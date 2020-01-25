namespace CSGOStats.Infrastructure.Core.Data.Entities
{
    public interface IEntityFactory<out TEntity, in TKey>
    {
        TEntity CreateEmpty(TKey id);
    }
}