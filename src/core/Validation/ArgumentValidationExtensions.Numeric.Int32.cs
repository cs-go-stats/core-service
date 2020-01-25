using System.Globalization;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class ArgumentValidationExtensions
    {
        public static int GreaterThan(this int instance, int ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i > g,
                instance,
                ethalon,
                $"'{{0}}' should be greater than ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.GreaterThan);

        public static int GreaterThanOrEqual(this int instance, int ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i >= g,
                instance,
                ethalon,
                $"'{{0}}' should be greater or equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.GreaterThanOrEqual);

        public static int LessThan(this int instance, int ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i < g,
                instance,
                ethalon,
                $"'{{0}}' should be less than ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.LessThan);

        public static int LessThanOrEqual(this int instance, int ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i <= g,
                instance,
                ethalon,
                $"'{{0}}' should be less or equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.LessThanOrEqual);

        public static int EqualTo(this int instance, int ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i == g,
                instance,
                ethalon,
                $"'{{0}}' should be equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.EqualTo);

        public static int NotEqualTo(this int instance, int ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i != g,
                instance,
                ethalon,
                $"'{{0}}' should not be equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.NotEqualTo);

        public static int Positive(this int instance, in string argumentName) =>
            ValidateNumber(
                i => i > 0,
                instance,
                "'{0}' should be a positive number.",
                argumentName,
                PreconditionCodes.Positive);

        public static int Negative(this int instance, in string argumentName) =>
            ValidateNumber(
                i => i < 0,
                instance,
                "'{0}' should not be a negative number.",
                argumentName,
                PreconditionCodes.Negative);

        public static int Natural(this int instance, in string argumentName) =>
            ValidateNumber(
                i => i >= 0,
                instance,
                "'{0}' should not be a natural number.",
                argumentName,
                PreconditionCodes.Natural);

        public static int InRange(this int instance, int lowerBound, int higherBound, in string argumentName) =>
            ValidateNumber(
                i => i >= lowerBound && i <= higherBound,
                instance,
                $"'{{0}}' should have a value between '{lowerBound}' and '{higherBound}'.",
                argumentName,
                PreconditionCodes.InRange);

        private static string Stringify(this int value) => value.ToString(CultureInfo.CurrentCulture);
    }
}