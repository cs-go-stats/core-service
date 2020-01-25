using System;

namespace CSGOStats.Infrastructure.Core.Validation
{
    public static partial class InstanceValidationExtensions
    {
        public static T AnythingBut<T>(this T instance, T unwantedValue, string instanceName)
            where T : IEquatable<T> =>
                instance.Equals(unwantedValue)
                    ? throw new UnwantedState(instanceName, unwantedValue)
                    : instance;
    }
}