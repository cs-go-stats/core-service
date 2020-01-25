using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF.Conversion
{
    public class OffsetDateTimeConverter : ValueConverter<OffsetDateTime, string>
    {
        public OffsetDateTimeConverter()
            : base(
                offsetDateTime => offsetDateTime.Serialize(),
                @string => @string.Deserialize())
        {
        }
    }
}