using System;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages
{
    public static class MessageBuilder
    {
        private static readonly Random Random = new Random();

        public static TestMessage GenerateRandomMessage() => new TestMessage(
            id: Guid.NewGuid().ToString("D"),
            version: Random.Next(minValue: 1, maxValue: int.MaxValue),
            data: GenerateRandomData());

        public static TestData GenerateRandomData() => new TestData(
            date: DateTime.UtcNow,
            time: TimeSpan.FromMilliseconds(Random.NextDouble() * Random.Next()));
    }
}