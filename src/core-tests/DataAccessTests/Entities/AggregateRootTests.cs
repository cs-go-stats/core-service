using System;
using CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Entities
{
    public class AggregateRootTests
    {
        [Fact]
        public void AggregateRoomMethods()
        {
            var id = Guid.NewGuid();
            var version = (long) new Random().Next();
            var aggregate = new TestAggregate(id, version);

            aggregate.UpdatePublic();

            aggregate.Id.Should().Be(id);
            aggregate.Version.Should().BeGreaterThan(version);
            aggregate.Version.Should().Be(version + 1);
        }
    }
}