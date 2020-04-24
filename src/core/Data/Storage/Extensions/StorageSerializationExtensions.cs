using System.Globalization;
using NodaTime;
using NodaTime.Text;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Extensions
{
    public static class SerializationExtensions
    {
        public static string Serialize(this OffsetDateTime x) =>
            x.ToString("r", CultureInfo.InvariantCulture);

        public static OffsetDateTime Deserialize(this string x) =>
            OffsetDateTimePattern.FullRoundtrip.Parse(x).Value;

        public static string SerializeNullable(this OffsetDateTime? x) => x?.Serialize();

        public static OffsetDateTime? DeserializeNullable(this string x) => x?.Deserialize();
    }
}