using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model
{
    [ModelLeaf]
    public class BodySubmodel
    {
        [AnchorLinkValue]
        public string Link { get; private set; }

        [ElementTitleValue]
        public string Title { get; private set; }
    }
}