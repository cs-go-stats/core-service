using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Markers;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model
{
    [ModelLeaf]
    public class HeaderModel
    {
        [Collection, RequiredContainer("div/ul[@class = 'numbers']/li"), IntegerValue]
        public WrappedCollection<int> Numbers { get; } = new WrappedCollection<int>();

        [Collection, RequiredContainer("div/ul[@class = 'texts']/li"), PlainTextValue]
        public WrappedCollection<string> Texts { get; } = new WrappedCollection<string>();

        [RequiredContainer("span"), ElementClassValue]
        public string Span { get; private set; }
    }
}