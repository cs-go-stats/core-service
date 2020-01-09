using CSGOStats.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSGOStats.Services.Core.Data
{
    public static class TypeRegistrationExtensions
    {
        public static EntityTypeBuilder<TEntity> RegisterTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema)
            where TEntity : class, IEntity =>
                builder.ToTable(typeof(TEntity).Name, schema);
    }
}