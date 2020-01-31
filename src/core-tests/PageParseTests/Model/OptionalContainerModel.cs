using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model
{
    [ModelLeaf]
    public class OptionalContainerModel
    {
        [RequiredContainer("p"), PlainTextValue]
        public string Text { get; set; }

        [RequiredContainer("span"), IntegerValue]
        public int Number { get; set; }
    }
}