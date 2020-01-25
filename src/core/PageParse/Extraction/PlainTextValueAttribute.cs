using System;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard;

namespace CSGOStats.Infrastructure.Core.PageParse.Extraction
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PlainTextValueAttribute : BaseMapValueAttribute
    {
        public PlainTextValueAttribute() 
            : base(StringValueMapper.StringValueCode)
        {
        }
    }
}