using System.Globalization;
using NodaTime;
using NodaTime.Text;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class SerializationExtensions
    {
        public static string Serialize(this OffsetDateTime x) =>
            x.ToString("r", CultureInfo.InvariantCulture);

        public static OffsetDateTime Deserialize(this string x) =>
            OffsetDateTimePattern.FullRoundtrip.Parse(x).Value;
    }
}