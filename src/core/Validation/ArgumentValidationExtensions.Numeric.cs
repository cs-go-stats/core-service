using System;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class ArgumentValidationExtensions
    {
        private static T ValidateNumber<T>(
            in Func<T, bool> validatorFunctor,
            T instance,
            in string validationMessage,
            in string argumentName,
            in string preconditionCode) =>
                validatorFunctor.NotNull(nameof(validatorFunctor)).Invoke(instance)
                    ? instance
                    : throw PreconditionFailed.ForCase(validationMessage, argumentName.EnsureArgumentName(), preconditionCode.EnsurePreconditionCode());

        private static T ValidateNumber<T>(
            in Func<T, T, bool> validatorFunctor,
            T instance,
            T ethalon,
            in string validationMessage,
            in string argumentName,
            in string ethalonValue,
            in string preconditionCode) =>
                validatorFunctor.NotNull(nameof(validatorFunctor)).Invoke(instance, ethalon)
                    ? instance
                    : throw PreconditionFailed.ForEthalon(validationMessage, argumentName.EnsureArgumentName(), ethalonValue, preconditionCode.EnsurePreconditionCode());
    }
}