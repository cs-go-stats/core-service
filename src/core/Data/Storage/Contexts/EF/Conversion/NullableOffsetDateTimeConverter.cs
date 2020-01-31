using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF.Conversion
{
    internal class NullableOffsetDateTimeConverter : ValueConverter<OffsetDateTime?, string>
    {
        public NullableOffsetDateTimeConverter()
            : base(
                offsetDateTime => offsetDateTime.SerializeNullable(),
                @string => @string.DeserializeNullable())
        {
        }
    }
}