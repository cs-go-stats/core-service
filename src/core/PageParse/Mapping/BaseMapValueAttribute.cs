using System;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Mapping
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BaseMapValueAttribute : Attribute
    {
        public string MapperCode { get; }

        public BaseMapValueAttribute(string mapperCode)
        {
            MapperCode = mapperCode.NotNull(nameof(mapperCode));
        }
    }
}