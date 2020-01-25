using System;
using NodaTime;
using NodaTime.Extensions;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.Extensions
{
    public static class DatetimeUtils
    {
        public static OffsetDateTime GetCurrentDate => new OffsetDateTime(
            localDateTime: DateTime.UtcNow.ToLocalDateTime(),
            offset: Offset.Zero);

        public static OffsetDateTime GetEmptyDate => new OffsetDateTime();
    }
}