using CSGOStats.Infrastructure.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class EfTypeRegistrationExtensions
    {
        public static EntityTypeBuilder<TEntity> RegisterTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema)
            where TEntity : class, IEntity =>
                builder.ToTable(typeof(TEntity).Name, schema);
    }
}