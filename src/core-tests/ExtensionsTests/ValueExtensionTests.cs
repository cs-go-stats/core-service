using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Model.DerivedTypes;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ExtensionsTests
{
    public class ValueExtensionTests
    {
        [Fact]
        public void ClassOfTypeTest()
        {
            object origin = new Sample();
            Record.Exception(() => origin.OfType<Sample>()).Should().BeNull();
        }

        [Fact]
        public void StructureOfTypeTest()
        {
            object origin = 1L;
            Record.Exception(() => origin.OfType<long>()).Should().BeNull();
        }

        [Fact]
        public void DerivedOfTypeTest()
        {
            Base origin = new Derived();
            Derived result = null;
            Record.Exception(() => result = origin.OfType<Base, Derived>()).Should().BeNull();

            result.Should().NotBeNull();
            result.Data.Should().Be(origin.Data);
        }
    }
}