namespace CSGOStats.Infrastructure.Core.PageParse.Mapping
{
    public interface IValueMapperFactory
    {
        IValueMapper Create(string mapperCode);
    }
}