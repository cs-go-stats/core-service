namespace CSGOStats.Services.Core.Links
{
    public static class HltvLinkExtensions
    {
        private const string HltvRoot = "https://hltv.org";

        public static string HltvUri(this string x) => HltvRoot + x;
    }
}