using System;
using CSGOStats.Infrastructure.Core.Data.Entities;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model
{
    public class TestAggregate : AggregateRoot
    {
        public TestAggregate(Guid id, long version)
            : base(id, version)
        {
        }

        public void UpdatePublic() => Update();
    }
}