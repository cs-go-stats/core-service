using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping
{
    public interface IValueMapper
    {
        object Map(HtmlNode root);
    }
}