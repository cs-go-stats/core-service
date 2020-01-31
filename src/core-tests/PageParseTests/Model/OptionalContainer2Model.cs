using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model
{
    [ModelLeaf]
    public class OptionalContainer2Model
    {
        [OptionalContainer("p"), PlainTextValue]
        public string Text { get; private set; }
    }
}