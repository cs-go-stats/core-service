namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    internal class ElementTitleMapper : ElementAttributeMapper
    {
        internal const string ElementTitleValueCode = nameof(ElementTitleMapper);

        public ElementTitleMapper()
            : base("title")
        {
        }
    }
}