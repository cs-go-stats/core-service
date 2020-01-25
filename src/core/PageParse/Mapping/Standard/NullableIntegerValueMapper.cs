using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    public class NullableIntegerValueMapper : BaseIntegerValueMapper, IValueMapper
    {
        internal const string NullableIntegerValueCode = nameof(NullableIntegerValueMapper);

        public object Map(HtmlNode root) => TryMap(root);
    }
}