using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    public abstract class BaseIntegerValueMapper
    {
        protected int? TryMap(HtmlNode root) => int.TryParse(root.InnerText, out var result) ? result : (int?)null;
    }
}