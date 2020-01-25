using System.Text.RegularExpressions;
using CSGOStats.Infrastructure.Core.Validation;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ValidationTests
{
    public class RegexTests
    {
        [Fact]
        public void SucceededRegexTest()
        {
            new Regex(@"\d+").Match("123").ForSucceeded().Success.Should().BeTrue();
        }

        [Fact]
        public void FailedRegexTest()
        {
            Record.Exception(() => new Regex(@"\d").Match("ABCD").ForSucceeded()).Should().BeOfType<ExpressionNotMatched>();
        }
    }
}