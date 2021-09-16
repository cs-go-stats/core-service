using System;
using System.Linq;
using CSGOStats.Infrastructure.Core.Validation;

namespace core_tests_foundation.Values
{
    public static class RandomGenerator
    {
        private static readonly Random Random = new Random();
        private static readonly char[] Chars = "eariotnslcudpmhgb".ToCharArray();

        public static int GetInt(int minValue = 0, int maxValue = 1000) => Random.Next(minValue: minValue, maxValue: maxValue + 1);
        
        public static string GetWord(int length = 10, bool capitalizeFirstLetter = false)
        {
            length.GreaterThanOrEqual(0, nameof(length));
            return capitalizeFirstLetter 
                ? $"{char.ToUpper(GetRandomChar())}{new string(GetRandomChars(length - 1))}"
                : new string(GetRandomChars(length));
        } 

        private static char GetRandomChar() => Chars[Random.Next(Chars.Length)];

        private static char[] GetRandomChars(int length) => Enumerable.Repeat(0, length).Select(_ => GetRandomChar()).ToArray();
    }
}