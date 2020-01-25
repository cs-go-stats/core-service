using NodaTime;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static OffsetDate UtcDate(this LocalDate x) => new OffsetDate(x, Offset.Zero);

        public static bool GreaterThan(this OffsetDate x, OffsetDate y) => x.Date > y.Date;

        public static OffsetDateTime FromUnixTimestamp(this long x) =>
            Instant.FromUnixTimeMilliseconds(x).WithOffset(Offset.Zero);
    }
}