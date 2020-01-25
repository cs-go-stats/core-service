using System;
using CSGOStats.Infrastructure.Core.Validation;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ValidationTests
{
    public class DoubleTests
    {
        private static readonly Random RandomInstance = new Random();

        [Fact]
        public void BinaryCheckPositiveTests()
        {
            {
                var value = GenerateValue();
                Record.Exception(() => value.GreaterThan(-1d, nameof(value))).Should().BeNull();
            }

            {
                const double lowerBound = .1;
                var value1 = GenerateValue(lowerBound: lowerBound);
                Record.Exception(() => value1.GreaterThanOrEqual(lowerBound, nameof(value1))).Should().BeNull();

                const double value2 = lowerBound;
                Record.Exception(() => value2.GreaterThanOrEqual(lowerBound, nameof(value2))).Should().BeNull();
            }

            {
                var value = GenerateValue(higherBound: .99);
                Record.Exception(() => value.LessThan(1d, nameof(value))).Should().BeNull();
            }

            {
                const double higherBound = .99;
                var value1 = GenerateValue(higherBound: higherBound);
                Record.Exception(() => value1.LessThanOrEqual(higherBound, nameof(value1))).Should().BeNull();

                const double value2 = higherBound;
                Record.Exception(() => value2.LessThanOrEqual(higherBound, nameof(value2))).Should().BeNull();
            }
        }

        [Fact]
        public void BinaryCheckNegativeTests()
        {
            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.GreaterThan(1d, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.GreaterThan);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.GreaterThanOrEqual(1d, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.GreaterThanOrEqual);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.LessThan(-1d, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.LessThan);
            }

            {
                var value = GenerateValue();
                var exception = Record.Exception(() => value.LessThanOrEqual(-1d, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.LessThanOrEqual);
            }
        }

        [Fact]
        public void MultipleFactorCheckPositiveTests()
        {
            {
                const double lowerBound = -.99;
                const double higherBound = 99;
                var value = GenerateValue(lowerBound, higherBound);
                Record.Exception(() => value.InRange(lowerBound, higherBound, nameof(value))).Should().BeNull();
            }
        }

        [Fact]
        public void MultipleFactorCheckNegativeTests()
        {
            {
                const double lowerBound = -2d;
                const double higherBound = -1.01;
                var value = GenerateValue();
                var exception = Record.Exception(() => value.InRange(lowerBound, higherBound, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.InRange);
            }

            {
                const double lowerBound = 1.01;
                const double higherBound = 2d;
                var value = GenerateValue();
                var exception = Record.Exception(() => value.InRange(lowerBound, higherBound, nameof(value))).Should().BeOfType<PreconditionFailed>().Subject;
                exception.PreconditionCode.Should().Be(PreconditionCodes.InRange);
            }
        }

        private static double GenerateValue(double lowerBound = -1d, double higherBound = 1d)
        {
            double result;

            do
            {
                result = RandomInstance.NextDouble() * (RandomInstance.Next(0, 2) == 0 ? 1 : -1);
            } while (result < lowerBound || result > higherBound);

            return result;
        }
    }
}