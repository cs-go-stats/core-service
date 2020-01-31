using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Markers;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model
{
    [ModelLeaf]
    public class BodyModel
    {
        [Collection, OptionalContainer("div[@class = 'optional-container']/div")]
        public WrappedCollection<OptionalContainerModel> Containers { get; } = new WrappedCollection<OptionalContainerModel>();

        [RequiredContainer("a"), AnchorLinkValue]
        public string Link { get; private set; }

        [RequiredContainer("a/img"), ElementTitleValue]
        public string ImageTitle { get; private set; }

        [RequiredContainer("div[@class = 'sub']/a")]
        public BodySubmodel Submodel { get; private set; }

        [Collection, RequiredContainer("div[@class = 'optional-container-2']/div")]
        public WrappedCollection<OptionalContainer2Model> Containers2 { get; } = new WrappedCollection<OptionalContainer2Model>();
    }
}