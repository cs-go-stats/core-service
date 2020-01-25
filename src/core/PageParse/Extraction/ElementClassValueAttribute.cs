using System;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard;

namespace CSGOStats.Infrastructure.Core.PageParse.Extraction
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementClassValueAttribute : BaseMapValueAttribute
    {
        public ElementClassValueAttribute()
            : base(ElementClassMapper.ElementClassValueCode)
        {
        }
    }
}