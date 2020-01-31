using CSGOStats.Infrastructure.Core.Validation;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests
{
    public abstract class FixtureBasedTest : IClassFixture<CoreTestFixture>
    {
        protected readonly CoreTestFixture Fixture;

        protected FixtureBasedTest(CoreTestFixture fixture)
        {
            Fixture = fixture.NotNull(nameof(fixture));
        }
    }
}