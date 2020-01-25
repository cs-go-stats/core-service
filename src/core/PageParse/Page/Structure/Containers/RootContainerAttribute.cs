using System;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RootContainerAttribute : BaseContainerAttribute
    {
        public RootContainerAttribute(string rootPath)
            : base(rootPath)
        {
        }
    }
}