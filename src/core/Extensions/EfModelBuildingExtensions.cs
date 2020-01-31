using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF.Conversion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class EfModelBuildingExtensions
    {
        public static PropertyBuilder<OffsetDateTime> OffsetDateTime(this PropertyBuilder<OffsetDateTime> x) =>
            x.HasConversion(new OffsetDateTimeConverter());

        public static PropertyBuilder<OffsetDateTime?> OffsetDateTime(this PropertyBuilder<OffsetDateTime?> x) =>
            x.HasConversion(new NullableOffsetDateTimeConverter());
    }
}