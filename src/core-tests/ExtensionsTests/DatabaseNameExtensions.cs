using System;

namespace CSGOStats.Infrastructure.Core.Tests.ExtensionsTests
{
    public static class DatabaseNameExtensions
    {
        public static string UseTestDatabaseName(this string x) => $"{x}_{DateTime.UtcNow.Ticks}";
    }
}