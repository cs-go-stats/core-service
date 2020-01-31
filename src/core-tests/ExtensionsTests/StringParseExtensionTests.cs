using System;
using System.Globalization;
using CSGOStats.Infrastructure.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ExtensionsTests
{
    public class StringParseExtensionTests
    {
        private const string WrongNumericValue = "Abcd";
        private const string CorrectNumericValue = "12345";

        private static readonly Random Random = new Random();

        [Fact]
        public void IntParseTests()
        {
            {
                var value = Random.Next();
                var parsed = value.ToString(CultureInfo.DefaultThreadCurrentCulture).Int();
                parsed.Should().Be(value);
            }

            {
                Record.Exception(() => WrongNumericValue.Int()).Should().BeOfType<FormatException>();
            }
        }

        [Fact]
        public void LongParseTests()
        {
            {
                var value = (long) Random.Next();
                var parsed = value.ToString(CultureInfo.DefaultThreadCurrentCulture).Long();
                parsed.Should().Be(value);
            }

            {
                Record.Exception(() => WrongNumericValue.Long()).Should().BeOfType<FormatException>();
            }
        }

        [Fact]
        public void DoubleParseTests()
        {
            {
                var value = Random.NextDouble();
                var parsed = value.ToString(CultureInfo.DefaultThreadCurrentCulture).Double();
                parsed.Should().Be(value);
            }

            {
                const string value = ".5";
                var parsed = value.Double();
                parsed.Should().Be(.5);
            }

            {
                Record.Exception(() => WrongNumericValue.Long()).Should().BeOfType<FormatException>();
            }
        }

        [Fact]
        public void BoolParseTests()
        {
            {
                var value = Random.NextDouble() > .5;
                var parsed = value.ToString(CultureInfo.DefaultThreadCurrentCulture).Bool();
                parsed.Should().Be(value);
            }

            {
                const string value = "True";
                var parsed = value.Bool();
                parsed.Should().BeTrue();
            }

            {
                const string value = "true";
                var parsed = value.Bool();
                parsed.Should().BeTrue();
            }

            {
                Record.Exception(() => WrongNumericValue.Bool()).Should().BeOfType<FormatException>();
                Record.Exception(() => CorrectNumericValue.Bool()).Should().BeOfType<FormatException>();
            }
        }

        [Fact]
        public void TimespanParseTests()
        {
            {
                var value = DateTime.Now.TimeOfDay;
                var parsed = value.ToString().Timespan();
                parsed.Should().Be(value);
            }

            {
                var day = Random.Next(10);
                var hour = Random.Next(24);
                var minute = Random.Next(60);
                var second = Random.Next(60);
                var millisecond = Random.Next(1000);
                var value = $"{day}.{hour}:{minute}:{second}.{millisecond}";
                var parsed = value.Timespan();
                parsed.Should().Be(new TimeSpan(day, hour, minute, second, millisecond));
            }

            {
                var parsed = CorrectNumericValue.Timespan();
                parsed.Days.Should().Be(CorrectNumericValue.Int());
            }

            {
                Record.Exception(() => WrongNumericValue.Timespan()).Should().BeOfType<FormatException>();
            }
        }
    }
}