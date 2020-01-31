using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model
{
    public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired().OffsetDateTime();
        }
    }
}