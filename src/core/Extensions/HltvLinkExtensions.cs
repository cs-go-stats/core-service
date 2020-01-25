namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class HltvLinkExtensions
    {
        private const string HltvRoot = "https://hltv.org";

        public static string HltvUri(this string x) => HltvRoot + x;
    }
}