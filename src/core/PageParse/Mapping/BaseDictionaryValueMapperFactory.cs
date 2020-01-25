using CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping
{
    public class BaseDictionaryValueMapperFactory : IValueMapperFactory
    {
        public IValueMapper Create(string mapperCode)
        {
            switch (mapperCode)
            {
                case AnchorLinkMapper.AnchorLinkValueCode:
                    return new AnchorLinkMapper();
                case ElementClassMapper.ElementClassValueCode:
                    return new ElementClassMapper();
                case ElementTitleMapper.ElementTitleValueCode:
                    return new ElementTitleMapper();
                case IntegerValueMapper.IntegerValueCode:
                    return new IntegerValueMapper();
                case NullableIntegerValueMapper.NullableIntegerValueCode:
                    return new NullableIntegerValueMapper();
                case StringValueMapper.StringValueCode:
                    return new StringValueMapper();
                default:
                    return null;
            }
        }
    }
}