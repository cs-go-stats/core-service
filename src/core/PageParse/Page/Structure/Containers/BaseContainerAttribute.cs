using System;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers
{
    public abstract class BaseContainerAttribute : Attribute
    {
        public string Path { get; }

        protected BaseContainerAttribute(string path)
        {
            Path = path.NotNull(nameof(path));
        }
    }
}