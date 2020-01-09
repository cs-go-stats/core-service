namespace CSGOStats.Services.Core.Handling.Entities
{
    public interface IEntityFactory<out TEntity, in TKey>
    {
        TEntity CreateEmpty(TKey id);
    }
}