namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class ValueExtensions
    {
        public static T OfType<T>(this object instance) => 
            (T) instance;

        public static TResult OfType<TInstance, TResult>(this TInstance instance)
            where TResult : TInstance
                => (TResult) instance;
    }
}