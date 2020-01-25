namespace CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard
{
    internal class AnchorLinkMapper : ElementAttributeMapper
    {
        internal const string AnchorLinkValueCode = nameof(AnchorLinkMapper);

        public AnchorLinkMapper() 
            : base("href")
        {
        }
    }
}