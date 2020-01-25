namespace CSGOStats.Infrastructure.Core.PageParse.Page.Parse
{
    public enum ActionType
    {
        Unknown,
        BindMarkup,
        ExtractValue,
        BindMarkupAndExtractValue // TODO may union of Bind/Extract
    }
}