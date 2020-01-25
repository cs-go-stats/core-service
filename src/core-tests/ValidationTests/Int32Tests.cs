using System;
using CSGOStats.Infrastructure.Core.Validation;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ValidationTests
{
    public class Int32Tests
    {
        private static readonly Random RandomInstance = new Random();

        [Fact]
        public void BinaryCheckPositiveTests()
        {
            {
                var value = GenerateValue(lowerBound: 1);
                Record.Exception(() => value.GreaterThan(0, nameof(value))).Should().BeNull();
            }

            {
                const int lowerBound = 1;
                var value1 = GenerateValue(lowerBound: lowerBound);
                Record.Exception(() => value1.GreaterThanOrEqual(lowerBound, nameof(value1))).Should().BeNull();

                const int value2 = lowerBound;
                Record.Exception(() => value2.GreaterThanOrEqual(lowerBound, nameof(value2))).Should().BeNull();
            }

            {
                var value = GenerateValue(higherBound: 99);
                Record.Exception(() => value.LessThan(100, nameof(value))).Should().BeNull();
            }

            {
                const int higherBound = 99;
                var value1 = GenerateValue(higherBound: higherBound);
                Record.Exception(() => value1.LessThanOrEqual(higherBound, nameof(value1))).Should().BeNull();

                const int value2 = higherBound;
                Record.Exception(() => value2.LessThanOrEqual(higherBound, nameof(value2))).Should().BeNull();
            }

            {
                var value = GenerateValue();
                Record.Exception(() => value.EqualTo(value, nameof(value))).Should().BeNull();
            }

            {
                var value = GenerateValue(higherBound: 50);
                var negativeEthalon = GenerateValue(lowerBound: 51);
                Record.Exception(() => value.NotEqualTo(negativeEthalon, nameof(value))).Should().BeNull();
            }
        }

        [Fact]
        public void BinaryCheckNegativeTests()
        {
            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.GreaterThan(100, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.GreaterThan);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.GreaterThanOrEqual(100, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.GreaterThanOrEqual);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.LessThan(0, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.LessThan);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.LessThanOrEqual(0, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.LessThanOrEqual);
            }

            {
                var value = GenerateValue(higherBound: 50);
                var exception = Record.Exception(() => value.EqualTo(GenerateValue(lowerBound: 51), nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.EqualTo);
            }

            {
                var value = GenerateValue(higherBound: 50);
                var negativeEthalon = value;
                var exception = Record.Exception(() => value.NotEqualTo(negativeEthalon, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.NotEqualTo);
            }
        }

        [Fact]
        public void UnaryCheckPositiveTests()
        {
            {
                var value = GenerateValue(lowerBound: 1);
                Record.Exception(() => value.Positive(nameof(value))).Should().BeNull();
            }

            {
                var value = GenerateValue(lowerBound: -100, higherBound: 0);
                Record.Exception(() => value.Negative(nameof(value))).Should().BeNull();
            }

            {
                var value = GenerateValue();
                Record.Exception(() => value.Natural(nameof(value))).Should().BeNull();
            }
        }

        [Fact]
        public void UnaryCheckNegativeTests()
        {
            {
                var value = GenerateValue(lowerBound: -100, higherBound: 0);
                var exception = Record.Exception(() => value.Positive(nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.Positive);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.Negative(nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.Negative);
            }

            {
                var value = GenerateValue(lowerBound: -100, higherBound: -1);
                var exception = Record.Exception(() => value.Natural(nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.Natural);
            }
        }

        [Fact]
        public void MultipleFactorCheckPositiveTests()
        {
            {
                const int lowerBound = 1;
                const int higherBound = 99;
                var value = GenerateValue(lowerBound, higherBound);
                Record.Exception(() => value.InRange(lowerBound, higherBound, nameof(value))).Should().BeNull();
            }
        }

        [Fact]
        public void MultipleFactorCheckNegativeTests()
        {
            {
                const int lowerBound = -100;
                const int higherBound = -1;
                var value = GenerateValue();
                var exception = Record.Exception(() => value.InRange(lowerBound, higherBound, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.InRange);
            }

            {
                const int lowerBound = 101;
                const int higherBound = 200;
                var value = GenerateValue();
                var exception = Record.Exception(() => value.InRange(lowerBound, higherBound, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.InRange);
            }
        }

        private static int GenerateValue(int lowerBound = 0, int higherBound = 100) => RandomInstance.Next(lowerBound, higherBound + 1);
    }
}