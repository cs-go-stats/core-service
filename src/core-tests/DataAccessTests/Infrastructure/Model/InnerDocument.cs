using System;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model
{
    public class InnerDocument : ICloneable
    {
        public string Data { get; private set; }

        public int Count { get; private set; }

        public OffsetDateTime? LockDate { get; private set; }

        public InnerDocument(string data, int count, OffsetDateTime? lockDate)
        {
            Data = data.NotNull(nameof(data));
            Count = count;
            LockDate = lockDate;
        }

        public void Update()
        {
            Data = Guid.NewGuid().ToString("N");
            Count = Guid.NewGuid().GetHashCode();
            LockDate = DateTimeExtensions.GetCurrentDate;
        }

        public object Clone() => new InnerDocument(
            data: Data,
            count: Count,
            lockDate: null);
    }
}