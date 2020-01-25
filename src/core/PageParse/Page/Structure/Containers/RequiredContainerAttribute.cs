using System;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredContainerAttribute : BasePropertyContainerAttribute
    {
        public RequiredContainerAttribute(string path) 
            : base(path, true)
        {
        }
    }
}