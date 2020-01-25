using System.Globalization;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class ArgumentValidationExtensions
    {
        public static long GreaterThan(this long instance, long ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i > g,
                instance,
                ethalon,
                $"'{{0}}' should be greater than ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.GreaterThan);

        public static long GreaterThanOrEqual(this long instance, long ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i >= g,
                instance,
                ethalon,
                $"'{{0}}' should be greater or equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.GreaterThanOrEqual);

        public static long LessThan(this long instance, long ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i < g,
                instance,
                ethalon,
                $"'{{0}}' should be less than ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.LessThan);

        public static long LessThanOrEqual(this long instance, long ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i <= g,
                instance,
                ethalon,
                $"'{{0}}' should be less or equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.LessThanOrEqual);

        public static long EqualTo(this long instance, long ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i == g,
                instance,
                ethalon,
                $"'{{0}}' should be equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.EqualTo);

        public static long NotEqualTo(this long instance, long ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i != g,
                instance,
                ethalon,
                $"'{{0}}' should not be equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.NotEqualTo);

        public static long Positive(this long instance, in string argumentName) =>
            ValidateNumber(
                i => i > 0,
                instance,
                "'{0}' should be a positive number.",
                argumentName,
                PreconditionCodes.Positive);

        public static long Negative(this long instance, in string argumentName) =>
            ValidateNumber(
                i => i < 0,
                instance,
                "'{0}' should not be a negative number.",
                argumentName,
                PreconditionCodes.Negative);

        public static long Natural(this long instance, in string argumentName) =>
            ValidateNumber(
                i => i >= 0,
                instance,
                "'{0}' should not be a natural number.",
                argumentName,
                PreconditionCodes.Natural);

        public static long InRange(this long instance, long lowerBound, long higherBound, in string argumentName) =>
            ValidateNumber(
                i => i >= lowerBound && i <= higherBound,
                instance,
                $"'{{0}}' should have a value between '{lowerBound}' and '{higherBound}'.",
                argumentName,
                PreconditionCodes.InRange);

        private static string Stringify(this long value) => value.ToString(CultureInfo.CurrentCulture);
    }
}