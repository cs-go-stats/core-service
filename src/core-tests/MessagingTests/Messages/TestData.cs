using System;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages
{
    public class TestData
    {
        public DateTime Date { get; }

        public TimeSpan Time { get; }

        public TestData(DateTime date, TimeSpan time)
        {
            Date = date;
            Time = time;
        }
    }
}