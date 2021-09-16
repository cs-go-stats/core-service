using System;
using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests
{
    public class DummyPageParseTests : FixtureBasedTest
    {
        public DummyPageParseTests(CoreTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public Task ParseHtmlTestAsync()
        {
            return Fixture.RunAsync(new FunctorAction(ParseHtmlTestAction));
        }
            
        private static async Task ParseHtmlTestAction(IServiceProvider serviceProvider, IConfigurationRoot _)
        {
            var loader = serviceProvider.GetRequiredService<IContentLoader>();
            var parser = serviceProvider.GetRequiredService<IPageParser<TestPageModel>>();
            var result = await parser.ParseAsync(loader);

            result.Header.Numbers.Should().BeEquivalentTo(new[] { 1, 2, 3 });
            result.Header.Texts.Should().BeEquivalentTo("one", "two", "three");
            result.Header.Span.Should().Be("test-span");

            result.Body.Containers.Select(x => (x.Number, x.Text)).Should().BeEquivalentTo(Enumerable
                .Range(1, 2)
                .Select(i => (i, $"text {i}")));
            result.Body.Link.Should().Be("https://www.mozilla.org/en-US/");
            result.Body.ImageTitle.Should().Be("Mozilla");
            result.Body.Submodel.Link.Should().Be("https://www.mozilla.org/en-US/");
            result.Body.Submodel.Title.Should().Be("Mozilla");
            result.Body.Containers2.Select(x => x.Text).Should().BeEquivalentTo("Text 1", null, "Text 3");

            result.Footer.Should().Be("Footer text.");
        }
    }
}