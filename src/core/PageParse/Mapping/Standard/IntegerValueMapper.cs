using CSGOStats.Infrastructure.Core.Validation;
using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    public class IntegerValueMapper : BaseIntegerValueMapper, IValueMapper
    {
        internal const string IntegerValueCode = nameof(IntegerValueMapper);

        public object Map(HtmlNode root) => TryMap(root).NotNull(nameof(root.InnerText));
    }
}