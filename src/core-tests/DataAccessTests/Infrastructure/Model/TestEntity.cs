using System;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Extensions;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model
{
    public class TestEntity : IHaveIdEntity
    {
        public Guid Id { get; }

        public OffsetDateTime Date { get; private set; }

        public TestEntity() { /* EF */ }

        public TestEntity(Guid id, OffsetDateTime date)
        {
            Id = id;
            Date = date;
        }

        public void Update()
        {
            Date = DateTimeExtensions.GetCurrentDate;
        }

        public static TestEntity CreateEmpty() => new TestEntity(Guid.NewGuid(), DatetimeUtils.GetEmptyDate);
    }
}