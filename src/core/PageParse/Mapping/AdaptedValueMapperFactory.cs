using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping
{
    public abstract class AdaptedValueMapperFactory : IValueMapperFactory
    {
        private readonly IValueMapperFactory _initialValueMapperFactory;
        private readonly IValueMapperFactory _adaptedValueMapperFactory;

        protected AdaptedValueMapperFactory(IValueMapperFactory initialValueMapperFactory, IValueMapperFactory adaptedValueMapperFactory)
        {
            _initialValueMapperFactory = initialValueMapperFactory.NotNull(nameof(initialValueMapperFactory));
            _adaptedValueMapperFactory = adaptedValueMapperFactory.NotNull(nameof(adaptedValueMapperFactory));
        }

        public IValueMapper Create(string mapperCode) =>
            _initialValueMapperFactory.Create(mapperCode) ??
            _adaptedValueMapperFactory.Create(mapperCode);
    }
}