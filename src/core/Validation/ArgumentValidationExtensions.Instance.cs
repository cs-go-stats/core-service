using System;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class ArgumentValidationExtensions
    {
        public static T NotNull<T>(this T instance, in string argumentName)
            where T : class =>
                instance ?? throw new ArgumentNullException(argumentName.EnsureArgumentName());

        public static T NotNull<T>(this T? instance, in string argumentName)
            where T : struct =>
                instance ?? throw new ArgumentNullException(argumentName.EnsureArgumentName());
    }
}