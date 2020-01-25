using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class ArgumentValidationExtensions
    {
        private static string EnsureArgumentName(this string argumentName) =>
            argumentName ?? throw new NullReferenceException("Argument should have a value.");

        private static string EnsurePreconditionCode(this string preconditionCode)
        {
            if (preconditionCode == null)
            {
                throw new NullReferenceException("Precondition code should have a value.");
            }

            if (GetAllPublicConstantValues<string>(typeof(PreconditionCodes)).Contains(preconditionCode))
            {
                return preconditionCode;
            }

            throw new ArgumentOutOfRangeException(nameof(preconditionCode), $"'{preconditionCode}' is not part of PreconditionCodes constant set.");
        }

        private static IEnumerable<T> GetAllPublicConstantValues<T>(this IReflect type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T) x.GetRawConstantValue());
        }
    }
}