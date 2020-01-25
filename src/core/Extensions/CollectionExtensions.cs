using System.Collections.Generic;
using System.Linq;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToCollection<T>(this T instance)
        {
            yield return instance;
        }

        public static T[] ToArrayFast<T>(this IEnumerable<T> collection) =>
            collection as T[] ?? collection?.ToArray() ?? new T[0];
    }
}