using System.Collections.Generic;
using CSGOStats.Infrastructure.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ExtensionsTests
{
    public class CollectionExtensionTests
    {
        [Fact]
        public void ToCollectionTest()
        {
            var instance = new object();
            using var instanceEnumerator = instance.ToCollection().GetEnumerator();
            instanceEnumerator.MoveNext().Should().BeTrue();
            instanceEnumerator.Current.Should().Be(instance);
            instanceEnumerator.Current.GetHashCode().Should().Be(instance.GetHashCode());
            instanceEnumerator.MoveNext().Should().BeFalse();
        }

        [Fact]
        public void ArrayToArrayFastTest()
        {
            var origin = new[] { 3, 1, 2 };
            var result = origin.ToArrayFast();
            result.Should().BeEquivalentTo(origin);
            result.Should().ContainInOrder(origin);
        }

        [Fact]
        public void CollectionToArrayFastTest()
        {
            var origin = new List<int> { 3, 1, 2 };
            var result = origin.ToArrayFast();
            result.Should().BeEquivalentTo(origin);
            result.Should().ContainInOrder(origin);
        }

        [Fact]
        public void NullToArrayFastTest()
        {
            List<int> origin = null;
            var result = origin.ToArrayFast();
            result.Should().BeEmpty();
        }
    }
}