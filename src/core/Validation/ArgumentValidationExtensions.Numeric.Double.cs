using System.Globalization;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class ArgumentValidationExtensions
    {
        public static double GreaterThan(this double instance, double ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i > g,
                instance,
                ethalon,
                $"'{{0}}' should be greater than ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.GreaterThan);

        public static double GreaterThanOrEqual(this double instance, double ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i >= g,
                instance,
                ethalon,
                $"'{{0}}' should be greater or equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.GreaterThanOrEqual);

        public static double LessThan(this double instance, double ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i < g,
                instance,
                ethalon,
                $"'{{0}}' should be less than ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.LessThan);

        public static double LessThanOrEqual(this double instance, double ethalon, in string argumentName) =>
            ValidateNumber(
                (i, g) => i <= g,
                instance,
                ethalon,
                $"'{{0}}' should be less or equal to ethalon value: '{ethalon}'.",
                argumentName,
                ethalon.Stringify(),
                PreconditionCodes.LessThanOrEqual);

        public static double InRange(this double instance, double lowerBound, double greaterBound, in string argumentName) =>
            ValidateNumber(
                i => i >= lowerBound && i <= greaterBound,
                instance,
                $"'{{0}}' should have a value between '{lowerBound}' and '{greaterBound}'.",
                argumentName,
                PreconditionCodes.InRange);

        private static string Stringify(this double value) => value.ToString(CultureInfo.CurrentCulture);
    }
}