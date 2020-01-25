using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    public class StringValueMapper : IValueMapper
    {
        internal const string StringValueCode = nameof(StringValueMapper);

        public object Map(HtmlNode root) => root.InnerText;
    }
}