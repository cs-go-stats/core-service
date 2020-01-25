namespace CSGOStats.Infrastructure.Core.PageParse.Mapping
{
    public class BaseDictionaryAdaptedValueMapperFactory : AdaptedValueMapperFactory
    {
        public BaseDictionaryAdaptedValueMapperFactory(IValueMapperFactory adaptedValueMapperFactory) 
            : base(new BaseDictionaryValueMapperFactory(), adaptedValueMapperFactory)
        {
        }
    }
}