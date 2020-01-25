using CSGOStats.Infrastructure.Core.Validation;
using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    public class ElementAttributeMapper : IValueMapper
    {
        private readonly string _attributeName;

        public ElementAttributeMapper(string attributeName)
        {
            _attributeName = attributeName.NotNull(nameof(attributeName));
        }

        public virtual object Map(HtmlNode root) => root.GetAttributeValue(_attributeName, null);
    }
}