using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;

namespace CSGOStats.Infrastructure.Core.Tests.PageParseTests.Model
{
    [ModelRoot, RootContainer("*/body/div[@id = 'root']")]
    public class TestPageModel
    {
        [RequiredContainer("div[@id = 'header']")]
        public HeaderModel Header { get; set; }

        [RequiredContainer("div[@id = 'body']")]
        public BodyModel Body { get; set; }

        [RequiredContainer("div[@id = 'footer']/p"), PlainTextValue]
        public string Footer { get; set; }
    }
}