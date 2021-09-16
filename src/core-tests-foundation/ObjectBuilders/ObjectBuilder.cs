namespace core_tests_foundation.ObjectBuilders
{
    public abstract class ObjectBuilder { }
    
    public abstract class ObjectBuilder<T> : ObjectBuilder
    {
        public abstract T Build();
    }
}