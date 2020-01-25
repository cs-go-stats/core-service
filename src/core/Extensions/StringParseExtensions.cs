namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class StringParseExtensions
    {
        public static int Int(this string x) => 
            int.Parse(x);

        public static long Long(this string x) =>
            long.Parse(x);

        public static double Double(this string x) =>
            double.Parse(x);

        public static bool Bool(this string x) =>
            bool.Parse(x);
    }
}